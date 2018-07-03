using DependencyResolver.Modules;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace MilkteaAdminAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

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
    }
}
