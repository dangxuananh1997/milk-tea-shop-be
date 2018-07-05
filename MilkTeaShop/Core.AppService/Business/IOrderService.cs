namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IOrderService : IBaseService<Order>
    {
        Order GetOrder(params object[] keys);

        Order GetOrder(Expression<Func<Order, bool>> predicated, params Expression<Func<Order, object>>[] includes);

        IQueryable<Order> GetAllOrder(params Expression<Func<Order, object>>[] includes);

        void CreateOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Order order);

        void DeleteOrder(int orderId);

        void SaveOrderChanges();
    }
}
