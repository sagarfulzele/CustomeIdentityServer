using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using PSIIDS4.Models;

namespace PSIIDS4.Configuration
{
    public class AspIdProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public AspIdProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            
            var sub = context.Subject.FindFirst("sub")?.Value;
            context.RequestedClaimTypes.Append("role");

            if (sub != null)
            {
                var user = await _userManager.FindByIdAsync(sub);
                var cp = await _claimsFactory.CreateAsync(user);

                var claims = cp.Claims;
                List<System.Security.Claims.Claim> scc = new List<System.Security.Claims.Claim>();
                if (context.AllClaimsRequested == false || (context.RequestedClaimTypes != null && context.RequestedClaimTypes.Any()))
                {
                    //claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type) ).ToArray().AsEnumerable();
                    //claims = claims.Where(x => x.Type == "Role").ToArray().AsEnumerable();
                    //scc.Add();
                    //scc.Add();
                }

                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var locked = true;

            var sub = context.Subject.FindFirst("sub")?.Value;
            if (sub != null)
            {
                var user = await _userManager.FindByIdAsync(sub);
                if (user != null)
                {
                    locked = await _userManager.IsLockedOutAsync(user);
                }
            }

            context.IsActive = !locked;
        }
    }
}
