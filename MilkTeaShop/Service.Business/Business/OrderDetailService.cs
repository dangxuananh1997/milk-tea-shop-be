namespace Service.Business.Business
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.AppService.Business;
    using Core.ObjectModel.Entity;
    using Core.ObjectService.Repositories;

    public class OrderDetailService : BaseService<OrderDetail>, IOrderDetailService
    {
        public OrderDetailService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            base.Create(orderDetail);
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            base.Delete(orderDetail);
        }

        public void DeleteOrderDetail(int orderDetailId)
        {
            base.Delete(orderDetailId);
        }

        public IQueryable<OrderDetail> GetAllOrderDetail(params Expression<Func<OrderDetail, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public OrderDetail GetOrderDetail(params object[] keys)
        {
            return base.Get(keys);
        }

        public OrderDetail GetOrderDetail(Expression<Func<OrderDetail, bool>> predicated, params Expression<Func<OrderDetail, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public void SaveOrderDetailChanges()
        {
            base.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            base.Update(orderDetail);
        }
    }
}
