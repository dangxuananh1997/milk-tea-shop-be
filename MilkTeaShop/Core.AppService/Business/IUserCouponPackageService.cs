namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IUserCouponPackageService : IBaseService<UserCouponPackage>
    {
        UserCouponPackage GetUserCouponPackage(params object[] keys);

        UserCouponPackage GetUserCouponPackage(Expression<Func<UserCouponPackage, bool>> predicated, params Expression<Func<UserCouponPackage, object>>[] includes);

        IQueryable<UserCouponPackage> GetAllUserCouponPackage(params Expression<Func<UserCouponPackage, object>>[] includes);

        void CreateUserCouponPackage(UserCouponPackage userCouponPackage);

        void UpdateUserCouponPackage(UserCouponPackage userCouponPackage);

        void DeleteUserCouponPackage(UserCouponPackage userCouponPackage);

        void DeleteUserCouponPackage(int userCouponPackageId);

        void SaveUserCouponPackageChanges();
    }
}