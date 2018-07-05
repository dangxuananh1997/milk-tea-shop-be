namespace Service.Business.Business
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.AppService.Business;
    using Core.ObjectModel.Entity;
    using Core.ObjectService.Repositories;

    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateOrder(Order order)
        {
            base.Create(order);
        }

        public void DeleteOrder(Order order)
        {
            base.Delete(order);
        }

        public void DeleteOrder(int orderId)
        {
            base.Delete(orderId);
        }

        public IQueryable<Order> GetAllOrder(params Expression<Func<Order, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public Order GetOrder(params object[] keys)
        {
            return base.Get(keys);
        }

        public Order GetOrder(Expression<Func<Order, bool>> predicated, params Expression<Func<Order, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public void SaveOrderChanges()
        {
            base.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            base.Update(order);
        }
    }
}
