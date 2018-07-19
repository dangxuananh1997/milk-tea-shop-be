using API.MilkteaAdmin;
using API.MilkteaAdmin.App_Start;
using API.MilkteaAdmin.Provider;
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
using System.Reflection;

[assembly: OwinStartup(typeof(Startup))]

namespace API.MilkteaAdmin
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
                    NinjectWebCommon.Kernel.Get<IIdentityService>()),
            });

            //Middle
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
        }
    }
}