using API.MilkteaAdmin.Models;
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

namespace API.MilkteaAdmin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IPagination _pagination;

        public OrdersController(IOrderService orderService, IPagination pagination)
        {
            this._orderService = orderService;
            this._pagination = pagination;
        }

        /// <summary>
        /// Get list Order Paged
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
        /// <response code="200">Return Orders</response>
        /// <response code="400">Invalid Page Index (Negative)</response> 
        /// <response code="500">Fail to Retrieve Orders</response>
        [HttpGet]
        public IHttpActionResult Get(int pageIndex, string searchValue)
        {
            if (pageIndex <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_PAGEINDEX);
            }

            try
            {
                List<Order> orders;
                if (String.IsNullOrEmpty(searchValue))
                {
                    // GET ALL
                    orders = _orderService.GetAllOrder(o => o.CouponItems.Select(u => u.UserCouponPackage.CouponPackage),
                    o => o.OrderDetails.Select(p => p.ProductVariant.Product))
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();
                }
                else
                {
                    // GET SEARCH RESULT
                    orders = _orderService.GetAllOrder(o => o.CouponItems.Select(u => u.UserCouponPackage),
                    o => o.OrderDetails.Select(p => p.ProductVariant.Product))
                        .Where(o => o.ContactPhone.Contains(searchValue) 
                        || o.DeliveryAddress.Contains(searchValue) 
                        || o.CustomerName.Contains(searchValue))
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();
                }

                List<OrderVM> orderVMs = AutoMapper.Mapper.Map<List<Order>, List<OrderVM>>(orders);
                Pager<OrderVM> result = _pagination.ToPagedList<OrderVM>(pageIndex, ConstantDataManager.PAGESIZE, orderVMs);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Get Order By Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order</returns>
        /// <response code="200">Return Order</response>
        /// <response code="400">Invalid Id</response> 
        /// <response code="500">Fail to Retrieve Order</response> 
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }
            try
            {
                OrderVM result = AutoMapper.Mapper.Map<Order, OrderVM>
                    (_orderService.GetOrder(o => o.Id == id,
                    o => o.CouponItems.Select( u => u.UserCouponPackage),
                    o => o.OrderDetails.Select(p => p.ProductVariant.Product)));

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="cm">Order Create Model</param>
        /// <returns></returns>
        /// <response code="200">Return Created Order</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Create</response> 
        [HttpPost]
        public IHttpActionResult Create(OrderCM cm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Order model = AutoMapper.Mapper.Map<OrderCM, Order>(cm);
                _orderService.CreateOrder(model);
                _orderService.SaveOrderChanges();

                OrderVM result = AutoMapper.Mapper.Map<Order, OrderVM>(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="um">Order Update Model</param>
        /// <returns></returns>
        /// <response code="200">Return Updated Order</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Update</response> 
        [HttpPut]
        public IHttpActionResult Update(OrderUM um)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Order model = AutoMapper.Mapper.Map<OrderUM, Order>(um);
                foreach (var item in um.OrderDetails)
                {
                    item.OrderId = um.Id;
                }
                _orderService.UpdateOrder(model);
                _orderService.SaveOrderChanges();

                OrderVM result = AutoMapper.Mapper.Map<Order, OrderVM>(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateStatus(OrderUSM um)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Order order = _orderService.GetOrder(o => o.Id == um.Id);
                order.Status = um.Status;
                _orderService.UpdateOrder(order);
                _orderService.SaveOrderChanges();

                OrderVM result = AutoMapper.Mapper.Map<Order, OrderVM>(order);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Delete Order By Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns></returns>
        /// <response code="200">Return Created Order</response>
        /// <response code="400">Invalid Id</response> 
        /// <response code="500">Fail to Delete</response> 
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }
            try
            {
                _orderService.DeleteOrder(id);
                _orderService.SaveOrderChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
