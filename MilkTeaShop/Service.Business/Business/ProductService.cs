using System;
using System.Linq;
using System.Linq.Expressions;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using Core.ObjectService.Repositories;

namespace Service.Business.Business
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void CreateProduct(Product product)
        {
            base.Create(product);
        }

        public void DeleteProduct(Product product)
        {
            base.Delete(product);
        }

        public void DeleteProduct(int productId)
        {
            base.Delete(productId);
        }

        public IQueryable<Product> GetAllProduct(params Expression<Func<Product, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public Product GetProduct(params object[] keys)
        {
            return base.Get(keys);
        }

        public Product GetProduct(Expression<Func<Product, bool>> predicated, params Expression<Func<Product, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public Product GetProductAsNoTracking(Expression<Func<Product, bool>> predicated, params Expression<Func<Product, object>>[] includes)
        {
            return base.GetAsNoTracking(predicated, includes);
        }

        public void UpdateProduct(Product product)
        {
            base.Update(product);
        }

        public void SaveProductChanges()
        {
            base.SaveChanges();
        }
    }
}
