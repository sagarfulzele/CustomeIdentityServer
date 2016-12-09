using IdentityServer4.Models;
using System.Collections.Generic;

namespace PSIIDS4.Configuration
{
    //public class GlobalVariable 
    //{
    //    public string ConnectionString { get; set; }
    //    public GlobalVariable(string constr)
    //    {
    //        ConnectionString = constr;
    //    }
    //}

    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Hybrid Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequireConsent = false,
                    RedirectUris = new List<string>
                    {
                        //"http://10.9.80.95/signin-oidc",
                        "http://localhost:3308/signin-oidc" 
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        //"http://10.9.80.95/",
                        "http://localhost:3308/"
                    },

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        "api1",
                        "mastercatalog"
                    }
                },
                new Client
                {
                    ClientId = "MasterCatalogueComponentsManager",
                    ClientName = "Master Catalogue Components Manager",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequireConsent = false,
                    RedirectUris = new List<string>
                    {
                        "http://10.9.80.95:85/signin-oidc",
                        "http://localhost:63311/signin-oidc",
                        "http://mpr.psi-incontrol.com:85/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                       "http://10.9.80.95:85/",
                       "http://localhost:63311/",
                       "http://mpr.psi-incontrol.com:85/",
                    },                    
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        "api1",
                        "mastercatalog"
                    }
                },
                new Client
                {
                    ClientId = "roclient",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.OfflineAccess.Name
                        ,"api1"
                        ,"mastercatalog"
                    }
                }
            };
        }
    }
}