namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IOrderDetailService : IBaseService<OrderDetail>
    {
        OrderDetail GetOrderDetail(params object[] keys);

        OrderDetail GetOrderDetail(Expression<Func<OrderDetail, bool>> predicated, params Expression<Func<OrderDetail, object>>[] includes);

        IQueryable<OrderDetail> GetAllOrderDetail(params Expression<Func<OrderDetail, object>>[] includes);

        void CreateOrderDetail(OrderDetail orderDetail);

        void UpdateOrderDetail(OrderDetail orderDetail);

        void DeleteOrderDetail(OrderDetail orderDetail);

        void DeleteOrderDetail(int orderDetailId);

        void SaveOrderDetailChanges();
    }
}
