using API.MilkteaAdmin.Models;
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

        [HttpGet]
        public IHttpActionResult Get()
        {
            List<ProductVM> productVMs = AutoMapper.Mapper.Map<List<Product>, List<ProductVM>>(_productService.GetAllProduct().ToList());

            return Ok(productVMs);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var productVM = AutoMapper.Mapper.Map<Product, ProductVM>(_productService.GetProduct(id));

            return Ok(productVM);
        }

        [HttpPost]
        public IHttpActionResult Create(ProductCM cm)
        {
            Product product = AutoMapper.Mapper.Map<ProductCM, Product>(cm);
            _productService.CreateProduct(product);
            _productService.SaveProductChanges();

            return Ok(cm);
        }

        [HttpPut]
        public IHttpActionResult Update(ProductUM um)
        {
            Product product = AutoMapper.Mapper.Map<ProductUM, Product>(um);
            _productService.CreateProduct(product);
            _productService.SaveProductChanges();

            return Ok(um);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);

            return Ok();
        }
    }
}
