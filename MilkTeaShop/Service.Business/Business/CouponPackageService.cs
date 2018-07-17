using Core.AppService.Business;
using Core.ObjectModel.Entity;
using Core.ObjectService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business.Business
{
    public class CouponPackageService : BaseService<CouponPackage>, ICouponPackageService
    {
        public CouponPackageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateCouponPackage(CouponPackage couponPackage)
        {
            base.Create(couponPackage);
        }

        public void DeleteCouponPackage(CouponPackage couponPackage)
        {
            base.Delete(couponPackage);
        }

        public void DeleteCouponPackage(int couponPackageId)
        {
            base.Delete(couponPackageId);
        }

        public IQueryable<CouponPackage> GetAllCouponPackage(params Expression<Func<CouponPackage, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public CouponPackage GetCouponPackage(params object[] keys)
        {
            return base.Get(keys);
        }

        public CouponPackage GetCouponPackage(Expression<Func<CouponPackage, bool>> predicated, params Expression<Func<CouponPackage, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public CouponPackage GetCouponPackageAsNoTracking(Expression<Func<CouponPackage, bool>> predicated, params Expression<Func<CouponPackage, object>>[] includes)
        {
            return base.GetAsNoTracking(predicated, includes);
        }

        public void SaveCouponPackageChanges()
        {
            base.SaveChanges();
        }

        public void UpdateCouponPackage(CouponPackage couponPackage)
        {
            base.Update(couponPackage);
        }
    }
}
