using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MilkteaAdmin.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IHttpActionResult GetAllProduct()
        {
            List<Product> products = _productService.GetAllProduct().ToList();

            return Ok(products);
        }
    }
}
