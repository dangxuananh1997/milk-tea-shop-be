using API.MilkteaAdmin.ConstantManager;
using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API.MilkteaAdmin.Controllers
{
    public class CouponPackagesController : ApiController
    {
        private readonly ICouponPackageService _couponPackageService;

        public CouponPackagesController(ICouponPackageService couponPackageService)
        {
            this._couponPackageService = couponPackageService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                List<CouponPackageVM> result = AutoMapper.Mapper.Map<List<CouponPackage>, List<CouponPackageVM>>
                (_couponPackageService.GetAllCouponPackage().ToList());

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
                CouponPackageVM result = AutoMapper.Mapper.Map<CouponPackage, CouponPackageVM>
                    (_couponPackageService.GetCouponPackage(id));
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(CouponPackageCM cm)
        {
            try
            {
                CouponPackage model = AutoMapper.Mapper.Map<CouponPackageCM, CouponPackage>(cm);
                _couponPackageService.CreateCouponPackage(model);
                _couponPackageService.SaveCouponPackageChanges();

                CouponPackageVM result = AutoMapper.Mapper.Map<CouponPackage, CouponPackageVM>(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(CouponPackageUM um)
        {
            try
            {
                CouponPackage model = AutoMapper.Mapper.Map<CouponPackageUM, CouponPackage>(um);
                _couponPackageService.UpdateCouponPackage(model);
                _couponPackageService.SaveCouponPackageChanges();

                CouponPackageVM result = AutoMapper.Mapper.Map<CouponPackage, CouponPackageVM>(model);
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
                _couponPackageService.DeleteCouponPackage(id);
                _couponPackageService.SaveCouponPackageChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
