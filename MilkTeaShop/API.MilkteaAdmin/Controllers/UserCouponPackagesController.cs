using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace API.MilkteaAdmin.Controllers
{
    [Authorize(Roles = "Administrator")]
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
        public IHttpActionResult Get()
        {
            try
            {
                List<UserCouponPackageVM> result = AutoMapper.Mapper.Map<List<UserCouponPackage>, List<UserCouponPackageVM>>
                    (_userCouponPackageService.GetAllUserCouponPackage().ToList());

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
                UserCouponPackageVM result = AutoMapper.Mapper.Map<UserCouponPackage, UserCouponPackageVM>
                    (_userCouponPackageService.GetUserCouponPackage(id));

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(UserCouponPackageCM cm)
        {
            try
            {
                UserCouponPackage model = AutoMapper.Mapper.Map<UserCouponPackageCM, UserCouponPackage>(cm);
                model.CouponItems = new List<CouponItem>();

                // Current hour, minute, second
                int cHour = model.PurchasedDate.Hour;
                int cMinute = model.PurchasedDate.Minute;
                int cSecond = model.PurchasedDate.Second;

                // CREATE 30 coupon item for this user package
                for (int i = 0; i < 30; i++)
                {
                    CouponItem newCouponItem = new CouponItem()
                    {
                        IsUsed = false,
                        DateExpired = model.PurchasedDate.AddDays(i)
                                                    .AddHours(60 - cHour - 1)
                                                    .AddMinutes(60 - cMinute - 1)
                                                    .AddSeconds(60 - cSecond - 1),
                        OrderId = null
                    };
                    model.CouponItems.Add(newCouponItem);
                }
                // CREATE USER PACKAGE
                _userCouponPackageService.CreateUserCouponPackage(model);
                _userCouponPackageService.SaveUserCouponPackageChanges();

                UserCouponPackageVM result = AutoMapper.Mapper.Map<UserCouponPackage, UserCouponPackageVM>(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(UserCouponPackageUM um)
        {
            try
            {
                UserCouponPackage model = AutoMapper.Mapper.Map<UserCouponPackageUM, UserCouponPackage>(um);
                _userCouponPackageService.UpdateUserCouponPackage(model);
                _userCouponPackageService.SaveUserCouponPackageChanges();

                UserCouponPackageVM result = AutoMapper.Mapper.Map<UserCouponPackage, UserCouponPackageVM>(model);
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
                _userCouponPackageService.DeleteUserCouponPackage(id);
                _userCouponPackageService.SaveUserCouponPackageChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
