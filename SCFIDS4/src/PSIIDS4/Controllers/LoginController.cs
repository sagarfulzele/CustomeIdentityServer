using PSIIDS4.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Services.InMemory;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PSIIDS4.Configuration;

namespace PSIIDS4.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserInteractionService _interaction;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IUserInteractionService interaction,
             IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<LoginController>();
            _interaction = interaction;
            _claimsFactory = claimsFactory;
        }

        [HttpGet("login", Name = "Login")]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = new LoginViewModel();

            if (returnUrl != null)
            {
                var context = await _interaction.GetLoginContextAsync();
                if (context != null)
                {
                    vm.Username = context.LoginHint;
                    vm.ReturnUrl = returnUrl;
                }
            }

            if (vm.Password == null)
            {
                return View();
            }
            else
            {
                return View(vm);
            }
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                
                if (signInResult.Succeeded)
                { 
                    var user = await _userManager.FindByNameAsync(model.Username);

                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Login requires email confirmation.");
                        return View(model);
                    }
                    await IssueCookie(user, "idsvr", "password");
                    if (model.ReturnUrl != null && _interaction.IsValidReturnUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return Redirect("~/");                   
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }

        private async Task IssueCookie(ApplicationUser user, string idp, string amr)
        {

            var cp = await _claimsFactory.CreateAsync(user);
            /*
          var name = user.Claims.Where(x => x.ClaimType == JwtClaimTypes.Name).Select(x => x.ClaimValue).FirstOrDefault() ?? user.UserName;

          var claims = new Claim[] {
              new Claim(JwtClaimTypes.Subject, user.Id),
               new Claim(JwtClaimTypes.Role, user.Roles.ToString()),
              new Claim(JwtClaimTypes.Name, name),
              new Claim(JwtClaimTypes.IdentityProvider, idp),
              new Claim(JwtClaimTypes.AuthenticationTime, DateTime.UtcNow.ToEpochTime().ToString()),
          };
          var ci = new ClaimsIdentity(claims, amr, JwtClaimTypes.Name, JwtClaimTypes.Role);
          var cp = new ClaimsPrincipal(ci);
          */
            await HttpContext.Authentication.SignInAsync(Constants.DefaultCookieAuthenticationScheme, cp);
        }       

        [HttpGet("/external/{provider}", Name = "External")]
        public IActionResult External(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, new AuthenticationProperties
            {
                RedirectUri = "/ui/external-callback?returnUrl=" + returnUrl
            });
        }

        [HttpGet("/external-callback")]
        public async Task<IActionResult> ExternalCallback(string returnUrl)
        {
            var tempUser = await HttpContext.Authentication.AuthenticateAsync("Temp");
            if (tempUser == null)
            {
                throw new Exception();
            }

            var claims = tempUser.Claims.ToList();

            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            claims.Remove(userIdClaim);

            var provider = userIdClaim.Issuer;
            var userId = userIdClaim.Value;

            //todo: Update Later
            //var user = _loginService.FindByExternalProvider(provider, userId);
            //if (user == null)
            //{
            //    user = _loginService.AutoProvisionUser(provider, userId, claims);
            //}
            //await IssueCookie(user, provider, "external");
            await HttpContext.Authentication.SignOutAsync("Temp");

            if (returnUrl != null)
            {
                // todo: signin
                //return new SignInResult(signInId);
            }

            return Redirect("~/");

        }
    }
}
