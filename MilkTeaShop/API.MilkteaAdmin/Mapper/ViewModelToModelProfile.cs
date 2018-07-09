namespace API.MilkteaAdmin.Mapper
{
    using API.MilkteaAdmin.Models;
    using AutoMapper;
    using Core.ObjectModel.Entity;

    public class ViewModelToModelProfile : Profile
    {
        public ViewModelToModelProfile()
        {
            #region Product
            CreateMap<ProductCM, Product>()
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(m => m.Picture, map => map.MapFrom(vm => vm.Picture));

            CreateMap<ProductUM, Product>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(m => m.Picture, map => map.MapFrom(vm => vm.Picture));
            #endregion

            #region ProductVariant
            CreateMap<ProductVariantCM, ProductVariant>()
                .ForMember(m => m.ProductId, map => map.MapFrom(vm => vm.ProductId))
                .ForMember(m => m.Size, map => map.MapFrom(vm => vm.Size))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price));

            CreateMap<ProductVariantUM, ProductVariant>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.ProductId, map => map.MapFrom(vm => vm.ProductId))
                .ForMember(m => m.Size, map => map.MapFrom(vm => vm.Size))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price));
            #endregion

            #region User
            CreateMap<UserCM, User>()
                .ForMember(m => m.Username, map => map.MapFrom(vm => vm.Username))
                .ForMember(m => m.FullName, map => map.MapFrom(vm => vm.FullName));

            CreateMap<UserUM, User>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.Username, map => map.MapFrom(vm => vm.Username))
                .ForMember(m => m.FullName, map => map.MapFrom(vm => vm.FullName));

            CreateMap<RegisterBindingModel, User>()
                .ForMember(m => m.Username, map => map.MapFrom(vm => vm.Email));
            #endregion

            #region CouponPackage
            CreateMap<CouponPackageCM, CouponPackage>()
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(m => m.DrinkQuantity, map => map.MapFrom(vm => vm.DrinkQuantity))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price));

            CreateMap<CouponPackageUM, CouponPackage>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(m => m.DrinkQuantity, map => map.MapFrom(vm => vm.DrinkQuantity))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price));
            #endregion

            #region 

            #endregion
        }
    }
}