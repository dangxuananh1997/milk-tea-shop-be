using API.MilkteaClient.Models;
using Core.AppService.Business;
using Core.AppService.Pagination;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using Core.ObjectModel.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Web;

namespace API.MilkteaClient.Controllers
{
    [Authorize(Roles = "Member")]
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly ICouponItemService _couponItemService;
        private readonly IUserService _userService;
        private readonly IPagination _pagination;
        private readonly int CURRENT_USER_ID;


        public OrdersController(IOrderService orderService, ICouponItemService couponItemService, IUserService userService, IPagination pagination)
        {
            this._orderService = orderService;
            this._couponItemService = couponItemService;
            this._userService = userService;
            this._pagination = pagination;

            // Get the current requesting user
            string username = HttpContext.Current.User.Identity.GetUserName();
            CURRENT_USER_ID = _userService.GetUser(u => u.Username.Equals(username)).Id;
        }

        [HttpGet]
        public IHttpActionResult GetAll(int pageIndex)
        {
            if (pageIndex <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_PAGEINDEX);
            }

            try
            {
                List<Order> orders;

                // GET ALL
                orders = _orderService.GetAllOrder()
                    .Where(o => o.UserId == CURRENT_USER_ID)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();

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
        public IHttpActionResult GetById(int id)
        {

            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }
            try
            {
                OrderVM result = AutoMapper.Mapper.Map<Order, OrderVM>
                    (_orderService.GetOrder(o => o.Id == id && o.UserId == CURRENT_USER_ID,
                    o => o.OrderDetails
                    .Select(x => x.ProductVariant.Product)));

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

                model.OrderDate = DateTime.Now.ToUniversalTime()
                    .AddHours(ConstantDataManager.WorldTime.VIETNAM);
                model.Status = ConstantDataManager.OrderStatus.PENDING;
                model.UserId = CURRENT_USER_ID;

                _orderService.CreateOrder(model);
                _orderService.SaveOrderChanges();

                foreach (int couponItemId in cm.CouponItemIds)
                {
                    CouponItem coupon = _couponItemService.GetCouponItem(couponItemId);
                    coupon.OrderId = model.Id;
                    coupon.IsUsed = true;
                    _couponItemService.UpdateCouponItem(coupon);
                    _couponItemService.SaveCouponItemChanges();
                }

                model = _orderService.GetOrder(o => o.Id == model.Id, o => o.CouponItems, o => o.OrderDetails);

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

                foreach (int couponItemId in um.CouponItemIds)
                {
                    CouponItem coupon = _couponItemService.GetCouponItem(couponItemId);
                    coupon.OrderId = model.Id;
                    coupon.IsUsed = true;
                    _couponItemService.UpdateCouponItem(coupon);
                    _couponItemService.SaveCouponItemChanges();
                }

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
