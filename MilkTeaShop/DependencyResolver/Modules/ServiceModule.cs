using Core.AppService.Business;
using Ninject.Modules;
using Service.Business.Business;

namespace DependencyResolver.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
            Bind<IProductVariantService>().To<ProductVariantService>();
        }
    }
}
