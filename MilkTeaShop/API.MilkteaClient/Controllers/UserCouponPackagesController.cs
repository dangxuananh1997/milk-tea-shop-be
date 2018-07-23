using API.MilkteaClient.Models;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Pagination;
using Core.AppService.Pagination;

namespace API.MilkteaClient.Controllers
{
    [Authorize(Roles = "Member")]
    public class UserCouponPackagesController : ApiController
    {
        private readonly IUserCouponPackageService _userCouponPackageService;
        private readonly ICouponPackageService _couponPackageService;
        private readonly ICouponItemService _couponItemService;
        private readonly IUserService _userService;
        private readonly IPagination _pagination;
        private readonly int CURRENT_USER_ID;

        public UserCouponPackagesController(IUserCouponPackageService userCouponPackageService, ICouponPackageService couponPackageService, ICouponItemService couponItemService, IUserService userService, IPagination pagination)
        {
            this._userCouponPackageService = userCouponPackageService;
            this._couponPackageService = couponPackageService;
            this._couponItemService = couponItemService;
            this._userService = userService;
            this._pagination = pagination;

            // Get the current requesting user
            string username = HttpContext.Current.User.Identity.GetUserName();
            CURRENT_USER_ID = _userService.GetUserAsNoTracking(u => u.Username.Equals(username)).Id;
        }

        [HttpGet]
        public IHttpActionResult GetAll(int pageIndex)
        {
            try
            {
                List<UserCouponPackageVM> resultVMs = AutoMapper.Mapper.Map<List<UserCouponPackage>, List<UserCouponPackageVM>>
                    (_userCouponPackageService.GetAllUserCouponPackage(_ => _.CouponItems)
                    .Where(_ => _.UserId == CURRENT_USER_ID)
                    .ToList());

                Pager<UserCouponPackageVM> result = _pagination.ToPagedList<UserCouponPackageVM>(pageIndex, ConstantDataManager.PAGESIZE, resultVMs);
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
                    (_userCouponPackageService.GetUserCouponPackage(_ => _.Id == id && _.UserId == CURRENT_USER_ID, _ => _.CouponItems));

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
                model.PurchasedDate = DateTime.Now; // Get Current time
                model.UserId = CURRENT_USER_ID;

                CouponPackage package = _couponPackageService.GetCouponPackage(model.CouponPackageId);

                // set user package property
                model.DrinkQuantity = package.DrinkQuantity;
                model.Price = package.Price;

                // Current hour, minute, second
                int cHour = model.PurchasedDate.Hour;
                int cMinute = model.PurchasedDate.Minute;
                int cSecond = model.PurchasedDate.Second;

                // CREATE 30 coupon item for this user package
                model.CouponItems = new List<CouponItem>();
                for (int i = 0; i < 30; i++)
                {
                    CouponItem newCouponItem = new CouponItem()
                    {
                        IsUsed = false,
                        DateExpired = model.PurchasedDate.AddDays(i)
                                                    .AddHours(24 - cHour - 1)
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
    }
}
