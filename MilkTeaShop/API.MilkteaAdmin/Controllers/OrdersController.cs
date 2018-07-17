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
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IPagination _pagination;

        public OrdersController(IOrderService orderService, IPagination pagination)
        {
            this._orderService = orderService;
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
                List<Order> orders;
                if (String.IsNullOrEmpty(searchValue))
                {
                    // GET ALL
                    orders = _orderService.GetAllOrder().ToList();
                }
                else
                {
                    // GET SEARCH RESULT
                    orders = _orderService.GetAllOrder().ToList()/*.Where(p => p..Contains(searchValue)).ToList()*/;
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
                    (_orderService.GetOrder(o => o.Id == id, o => o.OrderDetails));

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(OrderCM cm)
        {
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

        [HttpPut]
        public IHttpActionResult Update(OrderUM um)
        {
            try
            {
                Order model = AutoMapper.Mapper.Map<OrderUM, Order>(um);
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
