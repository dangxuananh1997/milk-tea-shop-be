
namespace API.MilkteaAdmin.Mapper
{
    using API.MilkteaAdmin.Models;
    using AutoMapper;
    using Core.ObjectModel.Entity;

    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<Product, ProductVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Picture, map => map.MapFrom(m => m.Picture));

            CreateMap<ProductVariant, ProductVariantVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.ProductName, map => map.MapFrom(m => m.Product.Name))
                .ForMember(vm => vm.Size, map => map.MapFrom(m => m.Size))
                .ForMember(vm => vm.Price, map => map.MapFrom(m => m.Price));
        }
    }
}