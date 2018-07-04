using Core.AppService.Business;
using Core.AppService.Database.Identity;
using Infrastructure.Identity;
using Infrastructure.Identity.Adapter;
using Ninject.Modules;
using Service.Business.Business;

namespace DependencyResolver.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIdentityService>().To<IdentityAdapter>();
            Bind<IIdentityProvider>().To<IdentityProvider>();
            Bind<IProductService>().To<ProductService>();
            Bind<IProductVariantService>().To<ProductVariantService>();

        }
    }
}
