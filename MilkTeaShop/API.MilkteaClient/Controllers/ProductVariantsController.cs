using API.MilkteaClient.Models;
using Core.AppService.Business;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MilkteaClient.Controllers
{
    [Authorize(Roles = "Member")]
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
    }
}
