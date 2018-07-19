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
    public class CouponPackagesController : ApiController
    {
        private readonly ICouponPackageService _couponPackageService;
        private readonly IPagination _pagination;

        public CouponPackagesController(ICouponPackageService couponPackageService, IPagination pagination)
        {
            this._couponPackageService = couponPackageService;
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
                List<CouponPackage> couponPackages;
                if (String.IsNullOrEmpty(searchValue))
                {
                    // GET ALL
                    couponPackages = _couponPackageService.GetAllCouponPackage().ToList();
                }
                else
                {
                    // GET SEARCH RESULT
                    couponPackages = _couponPackageService.GetAllCouponPackage().Where(p => p.Name.Contains(searchValue)).ToList();
                }

                List<CouponPackageVM> couponPackageVMs = AutoMapper.Mapper.Map<List<CouponPackage>, List<CouponPackageVM>>(couponPackages);
                Pager<CouponPackageVM> result = _pagination.ToPagedList<CouponPackageVM>(pageIndex, ConstantDataManager.PAGESIZE, couponPackageVMs);
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
    }
}
