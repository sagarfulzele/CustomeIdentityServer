using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Services.InMemory;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PSIIDS4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace PSIIDS4.Controllers
{
    public class LogoutController : Controller
    {
        //private readonly IUserInteractionService _interaction;
        //private readonly List<InMemoryUser> _users;
        //public LogoutController(IUserInteractionService interaction, List<InMemoryUser> users)
        //{
        //    _interaction = interaction;
        //    _users = users;
        //}


        private readonly IUserInteractionService _interaction;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;


        public LogoutController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IUserInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<LoginController>();
            _interaction = interaction;
        }

        [HttpGet("logout", Name = "Logout")]
        public IActionResult Index(string logoutId)
        {
            ViewData["logoutId"] = logoutId;
            return View();
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(string logoutId)
        {
            await HttpContext.Authentication.SignOutAsync(Constants.DefaultCookieAuthenticationScheme);
             
            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            //Login.LoginService logot = new Login.LoginService(_users);
            //logot.RemoveUserFromMemory(HttpContext.User.Identity.Name.ToString());

            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel()
            {
                PostLogoutRedirectUri = logout.PostLogoutRedirectUri,
                ClientName = logout.ClientId,
                SignOutIframeUrl = logout.SignOutIFrameUrl
            };
            return View("LoggedOut", vm);
        }
    }
}
