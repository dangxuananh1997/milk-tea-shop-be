using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.MilkteaAdmin.Controllers
{
    [Authorize(Roles = "Administrator")]
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
            if (productId <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }
            try
            {
                // 
                List<ProductVariantVM> productVariantVMs = AutoMapper.Mapper
                    .Map<List<ProductVariant>, List<ProductVariantVM>>
                    (_productVariantService.GetAllProductVariant(_ => _.Product)
                    .Where(_ => _.ProductId == productId).ToList());
                return Ok(productVariantVMs);
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
            
        }

        [HttpPost]
        public IHttpActionResult Create(ProductVariantCM cm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductVariant productVariant = AutoMapper.Mapper.Map<ProductVariantCM, ProductVariant>(cm);
                _productVariantService.CreateProductVariant(productVariant);
                _productVariantService.SaveProductVariantChanges();

                return Ok(cm);
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(ProductVariantUM um)
        {
            try
            {
                ProductVariant productVariant = AutoMapper.Mapper.Map<ProductVariantUM, ProductVariant>(um);
                _productVariantService.UpdateProductVariant(productVariant);
                _productVariantService.SaveProductVariantChanges();
                return Ok(um);
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }

            try
            {
                _productVariantService.DeleteProductVariant(id);
                _productVariantService.SaveProductVariantChanges();

                return Ok();
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
