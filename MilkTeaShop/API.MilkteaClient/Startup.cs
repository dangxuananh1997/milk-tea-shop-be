using API.MilkteaClient;
using API.MilkteaClient.App_Start;
using API.MilkteaClient.Provider;
using Core.AppService.Database.Identity;
using DependencyResolver.Modules;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Modules;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace API.MilkteaClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            //Middleware
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                AllowInsecureHttp = true,
                Provider = new CustomOAuthorAuthorization(
                    NinjectWebCommon.Kernel.Get<IIdentityService>())
            });

            //Middle
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}