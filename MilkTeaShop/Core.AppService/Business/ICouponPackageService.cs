namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ICouponPackageService : IBaseService<CouponPackage>
    {
        CouponPackage GetCouponPackage(params object[] keys);

        CouponPackage GetCouponPackage(Expression<Func<CouponPackage, bool>> predicated, params Expression<Func<CouponPackage, object>>[] includes);

        CouponPackage GetCouponPackageAsNoTracking(Expression<Func<CouponPackage, bool>> predicated, params Expression<Func<CouponPackage, object>>[] includes);

        IQueryable<CouponPackage> GetAllCouponPackage(params Expression<Func<CouponPackage, object>>[] includes);

        void CreateCouponPackage(CouponPackage couponPackage);

        void UpdateCouponPackage(CouponPackage couponPackage);

        void DeleteCouponPackage(CouponPackage couponPackage);

        void DeleteCouponPackage(int couponPackageId);

        void SaveCouponPackageChanges();
    }
}
