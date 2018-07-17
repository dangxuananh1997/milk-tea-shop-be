using API.MilkteaClient;
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
        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    _kernel = new StandardKernel();
                    _kernel.Load(Assembly.GetExecutingAssembly());

                    List<NinjectModule> modules = new List<NinjectModule>()
                    {
                        new InfrastructureModule(),
                        new ServiceModule()
                    };
                    _kernel.Load(modules);
                }
                return _kernel;
            }
        }

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
                    Kernel.Get<IIdentityService>())
            });

            //Middle
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}