
namespace API.MilkteaAdmin.Mapper
{
    using API.MilkteaAdmin.Models;
    using AutoMapper;
    using Core.ObjectModel.Entity;
    using System.Web.Configuration;

    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<Product, ProductVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Picture, map => map.MapFrom(m => WebConfigurationManager.AppSettings["siteName"] + m.Picture));

            CreateMap<ProductVariant, ProductVariantVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.ProductName, map => map.MapFrom(m => m.Product.Name))
                .ForMember(vm => vm.Size, map => map.MapFrom(m => m.Size))
                .ForMember(vm => vm.Price, map => map.MapFrom(m => m.Price));

            CreateMap<User, UserVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Username, map => map.MapFrom(m => m.Username))
                .ForMember(vm => vm.FullName, map => map.MapFrom(m => m.FullName));

            CreateMap<CouponPackage, CouponPackageVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.DrinkQuantity, map => map.MapFrom(m => m.DrinkQuantity))
                .ForMember(vm => vm.Price, map => map.MapFrom(m => m.Price));
        }
    }
}