/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using IdentityServer4.Validation;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PSIIDS4.Models;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using System.Collections.Generic;

namespace PSIIDS4.Configuration
{



    public class ID4ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //private readonly User<ApplicationUser> _claimsFactory;
        private UserManager<ApplicationUser> _userManager;
        //private readonly IdentityServer4.Services.InMemory.InMemoryUser _user;
        private SignInManager<ApplicationUser> _signInManagar;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        // private readonly AspIdProfileService _aspIdProfileService;

        public ID4ResourceOwnerPasswordValidator(UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignInManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = UserManager;
            _signInManagar = SignInManager;
            _claimsFactory = claimsFactory;
        }
        public async Task<CustomGrantValidationResult> ValidateAsync(string userName, string password, ValidatedTokenRequest request)
        {

            // Below code is throwing error  
            // var signInResult = await _signInManagar.PasswordSignInAsync(userName, password, true, false);

            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                PasswordHasher passhash = new PasswordHasher();
                var passSucess = passhash.VerifyHashedPassword(user.PasswordHash, password);
                if (passSucess == PasswordVerificationResult.Success)
                {
                    var cp = await _claimsFactory.CreateAsync(user);
                    return new CustomGrantValidationResult(user.Id, "password", cp.Claims);
                }
                else
                    return (new CustomGrantValidationResult("Invalid username or password"));
            }
            return (new CustomGrantValidationResult("Invalid username or password"));
        }
    }
}