namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IProductService : IBaseService<Product>
    {
        Product GetProduct(params object[] keys);

        Product GetProduct(Expression<Func<Product, bool>> predicated, params Expression<Func<Product, object>>[] includes);

        Product GetProductAsNoTracking(Expression<Func<Product, bool>> predicated, params Expression<Func<Product, object>>[] includes);

        IQueryable<Product> GetAllProduct(params Expression<Func<Product, object>>[] includes);

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);

        void DeleteProduct(int productId);

        void SaveProductChanges();
    }
}
