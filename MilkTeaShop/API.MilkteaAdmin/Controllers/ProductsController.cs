using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.AppService.Pagination;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using Core.ObjectModel.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.MilkteaAdmin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IPagination _pagination;

        public ProductsController(IProductService productService, IPagination pagination)
        {
            this._productService = productService;
            this._pagination = pagination;
        }

        /// <summary>
        /// Get list Products Paged
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Get
        /// 
        /// </remarks>
        /// <param name="pageIndex"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        /// <response code="200">Return Products</response>
        /// <response code="400">Invalid Page Index</response> 
        /// <response code="500">Fail to Retrieve Products</response>
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
                Pager<ProductVM> result = _pagination.ToPagedList<ProductVM>(pageIndex, ConstantDataManager.PAGESIZE, productVMs);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Get
        /// 
        /// </remarks>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        /// <response code="200">Return Product</response>
        /// <response code="400">Invalid Id</response> 
        /// <response code="500">Fail to Retrieve Product</response>
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

        /// <summary>
        /// Create Product
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Post
        /// 
        /// </remarks>
        /// <param name="cm">Product Create Model</param>
        /// <returns></returns>
        /// <response code="200">Return Created Product</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Create Product</response>
        [HttpPost]
        public IHttpActionResult Create(ProductCM cm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Create product
                Product product = AutoMapper.Mapper.Map<ProductCM, Product>(cm);
                product.Picture = null;
                _productService.CreateProduct(product);
                _productService.SaveProductChanges();
                if (!String.IsNullOrEmpty(cm.Picture))
                {

                    // image stream
                    var bytes = Convert.FromBase64String(cm.Picture);
                    // physical server path
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Media/Product/");
                    Guid guid = Guid.NewGuid();
                    // SAVE IMAGE TO SERVER

                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                        image.Save(filePath + guid + ".jpg");
                    }
                    // UPDATE IMAGE PATH
                    product.Picture = "/Media/Product/" + guid + ".jpg";
                    _productService.UpdateProduct(product);
                    _productService.SaveProductChanges();
                }
                // RESPONSE
                ProductVM productVM = AutoMapper.Mapper.Map<Product, ProductVM>(product);
                return Ok(productVM);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Put
        /// 
        /// </remarks>
        /// <param name="um">Product Update Model</param>
        /// <returns></returns>
        /// <response code="200">Return Updated Product</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Update Product</response>
        [HttpPut]
        public IHttpActionResult Update(ProductUM um)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Product updateProduct = AutoMapper.Mapper.Map<ProductUM, Product>(um);
                Product oldProduct = _productService.GetProductAsNoTracking(p => p.Id == um.Id);

                if (!um.Picture.Contains("/Media/Product/") && !string.IsNullOrEmpty(um.Picture))
                {
                    // DELETE OLD PICTURE
                    // physical path to folder contain product picture
                    string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/Media/Product/");
                    // physical path to this product picture
                    string physicalPath = null;
                    if (!String.IsNullOrEmpty(oldProduct.Picture))
                    {
                        physicalPath = folderPath + oldProduct.Picture.Substring(oldProduct.Picture.LastIndexOf("/") + 1);
                    }
                    // delete old picture
                    if (File.Exists(physicalPath))
                    {
                        File.Delete(physicalPath);
                    }


                    // MAPPING NEW PICTURE
                    // new Guid
                    Guid newGuid = Guid.NewGuid();
                    // image stream
                    var bytes = Convert.FromBase64String(um.Picture);
                    // save image to server
                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                        image.Save(folderPath + newGuid + ".jpg");
                    }
                    updateProduct.Picture = "/Media/Product/" + newGuid + ".jpg";
                }
                else
                {
                    updateProduct.Picture = oldProduct.Picture;
                }

                // UPDATE
                _productService.UpdateProduct(updateProduct);
                _productService.SaveProductChanges();

                // RESPONSE
                ProductVM productVM = AutoMapper.Mapper.Map<Product, ProductVM>(updateProduct);
                return Ok(productVM);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Delete
        /// 
        /// </remarks>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        /// <response code="200">Return Empty</response>
        /// <response code="400">Invalid Product Id</response> 
        /// <response code="500">Fail to Delete Product</response>
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
