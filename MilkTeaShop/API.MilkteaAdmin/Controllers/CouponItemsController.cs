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
    public class CouponItemsController : ApiController
    {
        private readonly ICouponItemService _couponItemService;

        public CouponItemsController(ICouponItemService couponItemService)
        {
            this._couponItemService = couponItemService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                List<CouponItemVM> result = AutoMapper.Mapper.Map<List<CouponItem>, List<CouponItemVM>>
                    (_couponItemService.GetAllCouponItem().ToList());

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
            try
            {
                CouponItemVM result = AutoMapper.Mapper.Map<CouponItem, CouponItemVM>
                    (_couponItemService.GetCouponItem(id));

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(CouponItemCM cm)
        {
            try
            {
                CouponItem model = AutoMapper.Mapper.Map<CouponItemCM, CouponItem>(cm);
                _couponItemService.CreateCouponItem(model);
                _couponItemService.SaveCouponItemChanges();

                CouponItemVM result = AutoMapper.Mapper.Map<CouponItem, CouponItemVM>(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(CouponItemUM um)
        {
            try
            {
                CouponItem model = AutoMapper.Mapper.Map<CouponItemUM, CouponItem>(um);
                _couponItemService.UpdateCouponItem(model);
                _couponItemService.SaveCouponItemChanges();

                CouponItemVM result = AutoMapper.Mapper.Map<CouponItem, CouponItemVM>(model);
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
            try
            {
                _couponItemService.DeleteCouponItem(id);
                _couponItemService.SaveCouponItemChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
