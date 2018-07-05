using API.MilkteaAdmin.ConstantManager;
using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.AppService.Pagination;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.MilkteaAdmin.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IPagination _pagination;

        public ProductsController(IProductService productService, IPagination pagination)
        {
            this._productService = productService;
            this._pagination = pagination;
        }

        [HttpGet]
        public IHttpActionResult Get(int pageIndex, string searchValue)
        {
            System.Collections.Generic.List<Product> products;
            if (String.IsNullOrEmpty(searchValue))
            {
                // GET ALL
                products = _productService.GetAllProduct().ToList();
            }
            else
            {
                // GET SEARCH RESULT
                products = _productService.GetAllProduct().Where(p => p.Name.Contains(searchValue)).ToList();
            }


            List<ProductVM> productVMs = AutoMapper.Mapper.Map<System.Collections.Generic.List<Product>, System.Collections.Generic.List<ProductVM>>(products);
            List<ProductVM> result = _pagination.ToPagedList<ProductVM>(pageIndex, ConstantDataManager.PAGESIZE, productVMs);

            return Ok(result);
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
