namespace API.MilkteaAdmin.Mapper
{
    using API.MilkteaAdmin.Models;
    using AutoMapper;
    using Core.ObjectModel.Entity;
    using Infrastructure.Entity.Database;

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
                //.ForMember(m => m.Username, map => map.MapFrom(vm => vm.Username))
                .ForMember(m => m.FullName, map => map.MapFrom(vm => vm.FullName))
                .ForMember(m => m.Address, map => map.MapFrom(vm => vm.Address))
                .ForMember(m => m.Phone, map => map.MapFrom(vm => vm.Phone))
                .ForMember(m => m.Avatar, map => map.MapFrom(vm => vm.Avatar));

            CreateMap<RegisterModel, User>()
                .ForMember(m => m.Username, map => map.MapFrom(vm => vm.Username))
                .ForMember(m => m.Phone, map => map.MapFrom(vm => vm.Username));
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
            CreateMap<CouponItemCM, CouponItem>()
                .ForMember(m => m.DateExpired, map => map.MapFrom(vm => vm.DateExpired))
                .ForMember(m => m.IsUsed, map => map.MapFrom(vm => vm.IsUsed))
                .ForMember(m => m.UserPackageId, map => map.MapFrom(vm => vm.UserPackageId));

            CreateMap<CouponItemUM, CouponItem>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.DateExpired, map => map.MapFrom(vm => vm.DateExpired))
                .ForMember(m => m.IsUsed, map => map.MapFrom(vm => vm.IsUsed))
                .ForMember(m => m.UserPackageId, map => map.MapFrom(vm => vm.UserPackageId));
            #endregion

            #region UserCouponPackage
            CreateMap<UserCouponPackageCM, UserCouponPackage>()
                .ForMember(m => m.DrinkQuantity, map => map.MapFrom(vm => vm.DrinkQuantity))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price))
                .ForMember(m => m.UserId, map => map.MapFrom(vm => vm.UserId))
                .ForMember(m => m.CouponPackageId, map => map.MapFrom(vm => vm.CouponPackageId));

            CreateMap<UserCouponPackageUM, UserCouponPackage>()
                .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
                .ForMember(m => m.DrinkQuantity, map => map.MapFrom(vm => vm.DrinkQuantity))
                .ForMember(m => m.Price, map => map.MapFrom(vm => vm.Price))
                .ForMember(m => m.PurchasedDate, map => map.MapFrom(vm => vm.PurchasedDate))
                .ForMember(m => m.UserId, map => map.MapFrom(vm => vm.UserId))
                .ForMember(m => m.CouponPackageId, map => map.MapFrom(vm => vm.CouponPackageId));
            #endregion

            #region Order
            CreateMap<OrderCM, Order>()
                .ForMember(m => m.TotalPrice, map => map.MapFrom(vm => vm.TotalPrice))
                .ForMember(m => m.PaymentType, map => map.MapFrom(vm => vm.PaymentType))
                .ForMember(m => m.UserId, map => map.MapFrom(vm => vm.UserId))
                .ForMember(m => m.ContactPhone, map => map.MapFrom(m => m.ContactPhone))
                .ForMember(m => m.DeliveryAddress, map => map.MapFrom(m => m.DeliveryAddress))
                .ForMember(m => m.CustomerName, map => map.MapFrom(m => m.CustomerName))
                .ForMember(m => m.OrderDetails, map => map.MapFrom(vm => vm.OrderDetails));

            CreateMap<OrderUM, Order>()
                .ForMember(m => m.Id, map => map.MapFrom(m => m.Id))
                .ForMember(m => m.TotalPrice, map => map.MapFrom(m => m.TotalPrice))
                .ForMember(m => m.PaymentType, map => map.MapFrom(m => m.PaymentType))
                .ForMember(m => m.Status, map => map.MapFrom(m => m.Status))
                .ForMember(m => m.OrderDate, map => map.MapFrom(vm => vm.OrderDate))
                .ForMember(m => m.UserId, map => map.MapFrom(m => m.UserId))
                .ForMember(m => m.ContactPhone, map => map.MapFrom(m => m.ContactPhone))
                .ForMember(m => m.DeliveryAddress, map => map.MapFrom(m => m.DeliveryAddress))
                .ForMember(m => m.CustomerName, map => map.MapFrom(m => m.CustomerName))
                .ForMember(m => m.OrderDetails, map => map.MapFrom(vm => vm.OrderDetails));
            #endregion

            #region OrderDetail
            CreateMap<OrderDetailCM, OrderDetail>()
                .ForMember(m => m.ProductVariantId, map => map.MapFrom(m => m.ProductVariantId))
                .ForMember(m => m.Quantity, map => map.MapFrom(m => m.Quantity))
                .ForMember(m => m.UnitPrice, map => map.MapFrom(m => m.UnitPrice));

            CreateMap<OrderDetailUM, OrderDetail>()
                .ForMember(m => m.Id, map => map.MapFrom(m => m.Id))
                .ForMember(m => m.OrderId, map => map.MapFrom(m => m.OrderId))
                .ForMember(m => m.ProductVariantId, map => map.MapFrom(m => m.ProductVariantId))
                .ForMember(m => m.Quantity, map => map.MapFrom(m => m.Quantity))
                .ForMember(m => m.UnitPrice, map => map.MapFrom(m => m.UnitPrice));
            #endregion
        }
    }
}