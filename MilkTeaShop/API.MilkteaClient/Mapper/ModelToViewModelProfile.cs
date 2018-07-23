
namespace API.MilkteaAdmin.Mapper
{
    using API.MilkteaClient.Models;
    using AutoMapper;
    using Core.ObjectModel.Entity;
    using System.Web.Configuration;

    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            #region Product
            CreateMap<Product, ProductVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Picture, map => map.MapFrom(m => WebConfigurationManager.AppSettings["adminSiteName"] + m.Picture));
            #endregion

            #region ProductVariant
            CreateMap<ProductVariant, ProductVariantVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                //.ForMember(vm => vm.ProductName, map => map.MapFrom(m => m.Product.Name))
                .ForMember(vm => vm.Size, map => map.MapFrom(m => m.Size))
                .ForMember(vm => vm.Price, map => map.MapFrom(m => m.Price));

            CreateMap<ProductVariant, ProductVariantODVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.ProductName, map => map.MapFrom(m => m.Product.Name))
                .ForMember(vm => vm.Size, map => map.MapFrom(m => m.Size))
                .ForMember(vm => vm.Price, map => map.MapFrom(m => m.Price))
                .ForMember(vm => vm.Picture, map => map.MapFrom(m => WebConfigurationManager.AppSettings["adminSiteName"] + m.Product.Picture));
            #endregion

            #region User
            CreateMap<User, UserVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Username, map => map.MapFrom(m => m.Username))
                .ForMember(vm => vm.FullName, map => map.MapFrom(m => m.FullName))
                .ForMember(vm => vm.Address, map => map.MapFrom(m => m.Address))
                .ForMember(vm => vm.Phone, map => map.MapFrom(m => m.Phone))
                .ForMember(vm => vm.Avatar, map => map.MapFrom(
                    m => m.Avatar == null ? WebConfigurationManager.AppSettings["clientSiteName"] + "/Media/User/default-avatar.png"
                     :  WebConfigurationManager.AppSettings["clientSiteName"] + m.Avatar));
            #endregion

            #region CouponPackage
            CreateMap<CouponPackage, CouponPackageVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.DrinkQuantity, map => map.MapFrom(m => m.DrinkQuantity))
                .ForMember(vm => vm.Price, map => map.MapFrom(m => m.Price))
                .ForMember(vm => vm.Picture, map => map.MapFrom(m => WebConfigurationManager.AppSettings["adminSiteName"] + m.Picture));
            #endregion

            #region CouponItem
            CreateMap<CouponItem, CouponItemVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.DateExpired, map => map.MapFrom(m => m.DateExpired))
                .ForMember(vm => vm.IsUsed, map => map.MapFrom(m => m.IsUsed))
                .ForMember(vm => vm.UserCouponPackage, map => map.MapFrom(m => m.UserCouponPackage));
            #endregion

            #region UserCouponPackage
            CreateMap<UserCouponPackage, UserCouponPackageVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.DrinkQuantity, map => map.MapFrom(m => m.DrinkQuantity))
                .ForMember(vm => vm.Price, map => map.MapFrom(m => m.Price))
                .ForMember(vm => vm.PurchasedDate, map => map.MapFrom(m => m.PurchasedDate))
                .ForMember(vm => vm.UserId, map => map.MapFrom(m => m.UserId))
                .ForMember(vm => vm.CouponPackageId, map => map.MapFrom(m => m.CouponPackageId))
                .ForMember(vm => vm.CouponItems, map => map.MapFrom(m => m.CouponItems));
            #endregion

            #region Order
            CreateMap<Order, OrderVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.TotalPrice, map => map.MapFrom(m => m.TotalPrice))
                .ForMember(vm => vm.Status, map => map.MapFrom(m => m.Status))
                .ForMember(vm => vm.PaymentType, map => map.MapFrom(m => m.PaymentType))
                .ForMember(vm => vm.OrderDate, map => map.MapFrom(m => m.OrderDate))
                .ForMember(vm => vm.UserId, map => map.MapFrom(m => m.UserId))
                .ForMember(vm => vm.ContactPhone, map => map.MapFrom(m => m.ContactPhone))
                .ForMember(vm => vm.DeliveryAddress, map => map.MapFrom(m => m.DeliveryAddress))
                .ForMember(vm => vm.CustomerName, map => map.MapFrom(m => m.CustomerName))
                .ForMember(vm => vm.CouponItems, map => map.MapFrom(m => m.CouponItems))
                .ForMember(vm => vm.OrderDetails, map => map.MapFrom(m => m.OrderDetails));
            #endregion

            #region Order Detail
            CreateMap<OrderDetail, OrderDetailVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.ProductVariant, map => map.MapFrom(m => m.ProductVariant))
                .ForMember(vm => vm.Quantity, map => map.MapFrom(m => m.Quantity))
                .ForMember(vm => vm.UnitPrice, map => map.MapFrom(m => m.UnitPrice));
            #endregion
        }
    }
}