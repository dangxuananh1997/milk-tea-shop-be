using API.MilkteaClient.Models;
using Core.AppService.Business;
using Core.AppService.Pagination;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using Core.ObjectModel.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MilkteaClient.Controllers
{
    [Authorize(Roles = "Member")]
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
                Pager<ProductVM> result = _pagination.ToPagedList<ProductVM>(pageIndex, ConstantDataManager.PAGESIZE, productVMs);
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
    }
}
