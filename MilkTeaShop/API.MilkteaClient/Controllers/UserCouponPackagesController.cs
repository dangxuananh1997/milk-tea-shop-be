using API.MilkteaClient.Models;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MilkteaClient.Controllers
{
    public class UserCouponPackagesController : ApiController
    {
        private readonly IUserCouponPackageService _userCouponPackageService;
        private readonly ICouponItemService _couponItemService;

        public UserCouponPackagesController(IUserCouponPackageService userCouponPackageService, ICouponItemService couponItemService)
        {
            this._userCouponPackageService = userCouponPackageService;
            this._couponItemService = couponItemService;
        }

        [HttpGet]
        public IHttpActionResult GetAll(int userId)
        {
            try
            {
                List<UserCouponPackageVM> result = AutoMapper.Mapper.Map<List<UserCouponPackage>, List<UserCouponPackageVM>>
                    (_userCouponPackageService.GetAllUserCouponPackage(_ => _.UserId == userId).ToList());

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public IHttpActionResult GetSingle(int id)
        {
            try
            {
                UserCouponPackageVM result = AutoMapper.Mapper.Map<UserCouponPackage, UserCouponPackageVM>
                    (_userCouponPackageService.GetUserCouponPackage(id));

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
