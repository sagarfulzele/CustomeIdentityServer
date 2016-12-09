using IdentityServer4.Models;
using System.Collections.Generic;

namespace PSIIDS4.Configuration
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "api1",
                    DisplayName = "API 1",
                    Description = "API 1 features and data",
                    Type = ScopeType.Resource
                },
                new Scope
                {
                    Name = "mastercatalog",
                    DisplayName = "Master catalog",
                    Description = "For Manageing Master catalog",
                    Type = ScopeType.Resource
                }
            };
        }
    }
}