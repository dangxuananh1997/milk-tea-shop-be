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
            Bind<IPagination>().To<PaginationService>();
            Bind<IProductService>().To<ProductService>();
            Bind<IProductVariantService>().To<ProductVariantService>();
            Bind<ICouponPackageService>().To<CouponPackageService>();
            Bind<ICouponItemService>().To<CouponItemService>();
            Bind<IUserCouponPackageService>().To<UserCouponPackageService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IOrderDetailService>().To<OrderDetailService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
