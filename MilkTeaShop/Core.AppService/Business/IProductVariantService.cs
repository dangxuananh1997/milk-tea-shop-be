namespace Core.AppService.Business
{
    using Core.ObjectModel.Entity;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IProductVariantService : IBaseService<ProductVariant>
    {
        ProductVariant GetProductVariant(params object[] keys);

        ProductVariant GetProductVariant(Expression<Func<ProductVariant, bool>> predicated, params Expression<Func<ProductVariant, object>>[] includes);

        IQueryable<ProductVariant> GetAllProductVariant(params Expression<Func<ProductVariant, object>>[] includes);

        void CreateProductVariant(ProductVariant productVariant);

        void UpdateProductVariant(ProductVariant productVariant);

        void DeleteProductVariant(ProductVariant productVariant);

        void DeleteProductVariant(int productVariantId);

        void SaveProductVariantChanges();
    }
}
