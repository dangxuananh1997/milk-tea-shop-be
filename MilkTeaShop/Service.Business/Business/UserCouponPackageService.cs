namespace Service.Business.Business
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.AppService.Business;
    using Core.ObjectModel.Entity;
    using Core.ObjectService.Repositories;

    public class UserCouponPackageService : BaseService<UserCouponPackage>, IUserCouponPackageService
    {
        public UserCouponPackageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateUserCouponPackage(UserCouponPackage userCouponPackage)
        {
            base.Create(userCouponPackage);
        }

        public void DeleteUserCouponPackage(UserCouponPackage userCouponPackage)
        {
            base.Delete(userCouponPackage);
        }

        public void DeleteUserCouponPackage(int userCouponPackageId)
        {
            base.Delete(userCouponPackageId);
        }

        public IQueryable<UserCouponPackage> GetAllUserCouponPackage(params Expression<Func<UserCouponPackage, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public UserCouponPackage GetUserCouponPackage(params object[] keys)
        {
            return base.Get(keys);
        }

        public UserCouponPackage GetUserCouponPackage(Expression<Func<UserCouponPackage, bool>> predicated, params Expression<Func<UserCouponPackage, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public void SaveUserCouponPackageChanges()
        {
            base.SaveChanges();
        }

        public void UpdateUserCouponPackage(UserCouponPackage userCouponPackage)
        {
            base.Update(userCouponPackage);
        }
    }
}
