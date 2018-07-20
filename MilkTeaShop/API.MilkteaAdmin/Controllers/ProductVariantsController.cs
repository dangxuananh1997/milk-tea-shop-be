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

        /// <summary>
        /// Get ProductVariant By Product's Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Get
        /// 
        /// </remarks>
        /// <param name="productId">Product's Id</param>
        /// <returns></returns>
        /// <response code="200">Return ProductVariants</response>
        /// <response code="400">Invalid Product's Id</response> 
        /// <response code="500">Fail to Retrieve ProductVariants</response>
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

        /// <summary>
        /// Create ProductVariant
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Post
        /// 
        /// </remarks>
        /// <param name="cm">ProductVariant Create Model</param>
        /// <returns></returns>
        /// <response code="200">Return Created ProductVariant</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Create ProductVariant</response>
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

        /// <summary>
        /// Update ProductVariant
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Put
        /// 
        /// </remarks>
        /// <param name="um">ProductVariant Update Model</param>
        /// <returns></returns>
        /// <response code="200">Return Updated ProductVariant</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Update ProductVariant</response>
        [HttpPut]
        public IHttpActionResult Update(ProductVariantUM um)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

        /// <summary>
        /// Delete ProductVariant By Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Delete
        /// 
        /// </remarks>
        /// <param name="id">ProductVariant Id</param>
        /// <returns></returns>
        /// <response code="200">Return Empty</response>
        /// <response code="400">Invalid ProductVariant Id</response> 
        /// <response code="500">Fail to Delete ProductVariant</response>
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
