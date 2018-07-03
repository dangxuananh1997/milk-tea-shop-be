using DependencyResolver.Modules;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MilkteaAdminAPI;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Startup))]

namespace MilkteaAdminAPI
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

        private static IKernel CreateKernel()
        {
            return Kernel;
        }

        private static void WebApiConfiguration(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            EnableCorsAttribute cors = new EnableCorsAttribute(
                "*",
                "*",
                "*",
                "*");
            config.EnableCors(cors);
        }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfiguration(config);

            //Middleware
            //app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            //{
            //    TokenEndpointPath = new PathString("/Token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
            //    AllowInsecureHttp = true,
            //    Provider = new CustomOAuthorAuthorization(
            //        Kernel.Get<IIdentityService>())
            //});

            ////Middle
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //Middleware
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

        }
    }
}