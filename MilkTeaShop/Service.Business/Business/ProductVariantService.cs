
namespace Service.Business.Business
{
    using Core.AppService.Business;
    using Core.ObjectModel.Entity;
    using Core.ObjectService.Repositories;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class ProductVariantService : BaseService<ProductVariant>, IProductVariantService
    {
        public ProductVariantService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateProductVariant(ProductVariant productVariant)
        {
            if (this.GetProductVariant(_ => _.ProductId == productVariant.ProductId && _.Size == productVariant.Size) == null)
            {
                base.Create(productVariant);
            }
            else
            {
                throw new ArgumentException("Already existed a product of this size");
            }
        }

        public void DeleteProductVariant(ProductVariant productVariant)
        {
            base.Delete(productVariant);
        }

        public void DeleteProductVariant(int productVariantId)
        {
            base.Delete(productVariantId);
        }

        public IQueryable<ProductVariant> GetAllProductVariant(params Expression<Func<ProductVariant, object>>[] includes)
        {
            return base.GetAll(includes);
        }

        public ProductVariant GetProductVariant(params object[] keys)
        {
            return base.Get(keys);
        }

        public ProductVariant GetProductVariant(Expression<Func<ProductVariant, bool>> predicated, params Expression<Func<ProductVariant, object>>[] includes)
        {
            return base.Get(predicated, includes);
        }

        public void UpdateProductVariant(ProductVariant productVariant)
        {
            base.Update(productVariant);
        }

        public void SaveProductVariantChanges()
        {
            base.SaveChanges();
        }
    }
}
