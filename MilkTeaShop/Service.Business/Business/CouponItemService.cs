namespace Service.Business.Business
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.AppService.Business;
    using Core.ObjectModel.Entity;
    using Core.ObjectService.Repositories;

    public class CouponItemService : BaseService<CouponItem>, ICouponItemService
    {
        public CouponItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateCouponItem(CouponItem couponItem)
        {
            base.Create(couponItem);
        }

        public void DeleteCouponItem(CouponItem couponItem)
        {
            base.Delete(couponItem);
        }

        public void DeleteCouponItem(int couponItemId)
        {
            base.Delete(couponItemId);
        }

        public IQueryable<CouponItem> GetAllCouponItem(params Expression<Func<CouponItem, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public CouponItem GetCouponItem(params object[] keys)
        {
            return base.Get(keys);
        }

        public CouponItem GetCouponItem(Expression<Func<CouponItem, bool>> predicated, params Expression<Func<CouponItem, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public void SaveCouponItemChanges()
        {
            base.SaveChanges();
        }

        public void UpdateCouponItem(CouponItem couponItem)
        {
            base.Update(couponItem);
        }
    }
}
