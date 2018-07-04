namespace API.MilkteaAdmin.Mapper
{
    using API.MilkteaAdmin.Models;
    using AutoMapper;
    using Core.ObjectModel.Entity;

    public class ViewModelToModelProfile : Profile
    {
        public ViewModelToModelProfile()
        {
            CreateMap<ProductCM, Product>()
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(m => m.Picture, map => map.MapFrom(vm => vm.Picture));

            CreateMap<ProductUM, Product>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
                .ForMember(m => m.Picture, map => map.MapFrom(vm => vm.Picture));

            CreateMap<ProductVariantCM, ProductVariant>()
                .ForMember(m => m.ProductId, map => map.MapFrom(vm => vm.ProductId))
                .ForMember(m => m.Size, map => map.MapFrom(vm => vm.Size))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price));

            CreateMap<ProductVariantUM, ProductVariant>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.ProductId, map => map.MapFrom(vm => vm.ProductId))
                .ForMember(m => m.Size, map => map.MapFrom(vm => vm.Size))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price));
        }
    }
}