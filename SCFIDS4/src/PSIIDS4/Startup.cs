using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using PSIIDS4.Configuration;
using PSIIDS4.Data;
using PSIIDS4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.ActiveDirectory;
using Microsoft.AspNetCore.Authentication.ActiveDirectory.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace PSIIDS4
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
           
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
              //  builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
             
            
            Configuration = builder.Build();

            _environment = env;
        }
     
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services )
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            var cert =   new X509Certificate2(Path.Combine(_environment.ContentRootPath, "idsvr3test.pfx"), "idsrv3test");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Cookies.ApplicationCookie.AuthenticationScheme = "Cookies";
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.UserInteractionOptions.LoginUrl = "/login";
                options.UserInteractionOptions.LogoutUrl = "/logout";
                options.UserInteractionOptions.ConsentUrl = "/consent";
                options.UserInteractionOptions.ErrorUrl = "/error";
            })
            .SetSigningCredential(cert)
            .AddInMemoryClients(Clients.Get()) 
             .AddInMemoryScopes(Scopes.Get());
            //.AddInMemoryUsers(new List<ApplicationUser>());
             

            services.AddTransient<IProfileService, AspIdProfileService>();
            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, IdentityServerUserClaimsPrincipalFactory>();
            services.AddTransient<IResourceOwnerPasswordValidator, ID4ResourceOwnerPasswordValidator>();
             
            // for the UI
            services.AddMvc();
            //  services.AddAuthentication(options => new ActiveDirectoryCookieOptions());
            services.AddAuthentication();
            // services.AddTransient<UI.Login.LoginService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Temp",
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseIdentity();
            app.UseIdentityServer();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            /* 
            app.UseCookieAuthentication(new ActiveDirectoryCookieOptions().ApplicationCookie);
            app.UseNtlm(new ActiveDirectoryOptions
            {
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,
                AuthenticationScheme = ActiveDirectoryOptions.DefaultAuthenticationScheme,
                SignInAsAuthenticationScheme = ActiveDirectoryOptions.DefaultAuthenticationScheme,
                //Optionally, you can handle the events below
                Events = new AuthenticationEvents()
                {
                    OnAuthenticationSucceeded = succeededContext =>
                    {
                        var userName = succeededContext.Ticket.Principal.Identity.Name;
                        //do something on successful authentication
                        return Task.FromResult<object>(null);
                    },
                    OnAuthenticationFailed = failedContext =>
                    {
                        //do something on failed authentication
                        return Task.FromResult<object>(null);
                    }
                }
            });
            */

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                SignInScheme = "Temp",
                ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com",
                ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo"
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}