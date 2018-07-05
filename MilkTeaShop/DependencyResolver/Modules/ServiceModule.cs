using Core.AppService.Business;
using Core.AppService.Database.Identity;
using Core.AppService.Pagination;
using Infrastructure.Identity;
using Infrastructure.Identity.Adapter;
using Ninject.Modules;
using Service.Business.Business;
using Service.Business.Pagination;

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
            Bind<IPagination>().To<PaginationService>();

        }
    }
}
