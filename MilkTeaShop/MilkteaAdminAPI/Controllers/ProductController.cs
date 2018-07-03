using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MilkteaAdminAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IHttpActionResult GetAll()
        {
            List<Product> products = _productService.GetAllProduct().ToList();

            return Ok(products);
        }
    }
}
