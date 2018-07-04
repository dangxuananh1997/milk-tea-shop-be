using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API.MilkteaAdmin.Controllers
{
    public class ProductVariantsController : ApiController
    {
        private readonly IProductVariantService _productVariantService;

        public ProductVariantsController(IProductVariantService productVariantService)
        {
            this._productVariantService = productVariantService;
        }

        [HttpGet]
        public IHttpActionResult Get(int productId)
        {
            List<ProductVariantVM> productVariantVMs = AutoMapper.Mapper
                .Map<List<ProductVariant>, List<ProductVariantVM>>
                (_productVariantService.GetAllProductVariant(_ => _.Product).Where(_ => _.ProductId == productId).ToList());

            return Ok(productVariantVMs);
        }

        [HttpPost]
        public IHttpActionResult Create(ProductVariantCM cm)
        {
            ProductVariant productVariant = AutoMapper.Mapper.Map<ProductVariantCM, ProductVariant>(cm);
            _productVariantService.CreateProductVariant(productVariant);
            _productVariantService.SaveProductVariantChanges();

            return Ok(cm);
        }

        [HttpPut]
        public IHttpActionResult Update(ProductVariantUM um)
        {
            ProductVariant productVariant = AutoMapper.Mapper.Map<ProductVariantUM, ProductVariant>(um);
            _productVariantService.UpdateProductVariant(productVariant);
            _productVariantService.SaveProductVariantChanges();

            return Ok(um);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _productVariantService.DeleteProductVariant(id);
            _productVariantService.SaveProductVariantChanges();

            return Ok();
        }
    }
}
