using API.MilkteaAdmin.ConstantManager;
using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.AppService.Pagination;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.MilkteaAdmin.Controllers
{
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
            if (pageIndex <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_PAGEINDEX);
            }

            try
            {
                List<Product> products;
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

                List<ProductVM> productVMs = AutoMapper.Mapper.Map<List<Product>, List<ProductVM>>(products);
                List<ProductVM> result = _pagination.ToPagedList<ProductVM>(pageIndex, ConstantDataManager.PAGESIZE, productVMs);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }

            try
            {
                var productVM = AutoMapper.Mapper.Map<Product, ProductVM>(_productService.GetProduct(id));
                return Ok(productVM);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(ProductCM cm)
        {
            try
            {
                // Create product
                Product product = AutoMapper.Mapper.Map<ProductCM, Product>(cm);
                _productService.CreateProduct(product);
                _productService.SaveProductChanges();

                // image stream
                var bytes = Convert.FromBase64String(cm.Picture);
                // physical server path
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Media/Product/");
                // SAVE IMAGE TO SERVER
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    image.Save(filePath + product.Id + ".jpg");

                }

                // UPDATE IMAGE PATH
                product.Picture = "/Media/Product/" + product.Id + ".jpg";
                _productService.UpdateProduct(product);

                // RESPONSE
                ProductVM productVM = AutoMapper.Mapper.Map<Product, ProductVM>(product);
                return Ok(productVM);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }   
        }

        [HttpPut]
        public IHttpActionResult Update(ProductUM um)
        {
            try
            {
                // PHYSICAL PATH
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Media/Product/");
                // IMAGE STREAM
                var bytes = Convert.FromBase64String(um.Picture);
                // SAVE IMAGE TO SERVER
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    image.Save(filePath + um.Id + ".jpg");

                }

                // UPDATE
                Product product = AutoMapper.Mapper.Map<ProductUM, Product>(um);
                product.Picture = "/Media/Product/" + product.Id + ".jpg";

                _productService.UpdateProduct(product);
                _productService.SaveProductChanges();

                // RESPONSE
                ProductVM productVM = AutoMapper.Mapper.Map<Product, ProductVM>(product);
                return Ok(productVM);
            }
            catch (Exception e)
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
                // DELETE
                _productService.DeleteProduct(id);
                _productService.SaveProductChanges();
                // RESPONSE
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            
        }
    }
}
