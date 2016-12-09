using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using System.Web.Http;
 
[assembly: OwinStartup(typeof(PsiMprODataApi.Startup))]

namespace PsiMprODataApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://10.9.80.95:82/",
                //Authority = "http://localhost:53289/",
                RequiredScopes = new[] { "api1" }

            });

            app.UseWebApi(WebApiConfig.Register(new HttpConfiguration()));
        }
    }
}
