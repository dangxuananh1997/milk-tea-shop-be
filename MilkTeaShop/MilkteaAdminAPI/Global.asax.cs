using DependencyResolver.Modules;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common.WebHost;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace MilkteaAdminAPI
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            List<NinjectModule> modules = new List<NinjectModule>()
            {
                new InfrastructureModule(),
                new ServiceModule()
            };
            kernel.Load(modules);
            return kernel;
        }
    }
}
