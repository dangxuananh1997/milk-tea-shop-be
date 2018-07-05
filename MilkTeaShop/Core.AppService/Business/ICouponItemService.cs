namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ICouponItemService : IBaseService<CouponItem>
    {
        CouponItem GetCouponItem(params object[] keys);

        CouponItem GetCouponItem(Expression<Func<CouponItem, bool>> predicated, params Expression<Func<CouponItem, object>>[] includes);

        IQueryable<CouponItem> GetAllCouponItem(params Expression<Func<CouponItem, object>>[] includes);

        void CreateCouponItem(CouponItem couponItem);

        void UpdateCouponItem(CouponItem couponItem);

        void DeleteCouponItem(CouponItem couponItem);

        void DeleteCouponItem(int couponItemId);

        void SaveCouponItemChanges();
    }
}
