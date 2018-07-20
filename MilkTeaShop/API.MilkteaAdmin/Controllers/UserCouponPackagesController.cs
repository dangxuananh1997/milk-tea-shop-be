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

        /// <summary>
        /// Get All UserCouponPackages
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Get
        /// 
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Return UserCouponPackages</response>
        /// <response code="500">Fail to Retrieve UserCouponPackages</response>
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

        /// <summary>
        /// Get UserCouponPackage By Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Get
        /// 
        /// </remarks>
        /// <param name="id">UserCouponPackage Id</param>
        /// <returns></returns>
        /// <response code="200">Return UserCouponPackage</response>
        /// <response code="400">Invalid Id</response> 
        /// <response code="500">Fail to Retrieve UserCouponPackage</response>
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

        /// <summary>
        /// Create UserCouponPackage
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Post
        /// 
        /// </remarks>
        /// <param name="cm">UserCouponPackage Create Model</param>
        /// <returns></returns>
        /// <response code="200">Return Created UserCouponPackage</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Create UserCouponPackage</response>
        [HttpPost]
        public IHttpActionResult Create(UserCouponPackageCM cm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserCouponPackage model = AutoMapper.Mapper.Map<UserCouponPackageCM, UserCouponPackage>(cm);
                model.CouponItems = new List<CouponItem>();
                model.PurchasedDate = DateTime.Now;

                // Current hour, minute, second
                int cHour = model.PurchasedDate.Hour;
                int cMinute = model.PurchasedDate.Minute;
                int cSecond = model.PurchasedDate.Second;

                // CREATE 30 coupon item for this user package
                for (int i = 1; i <= 30; i++)
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

        /// <summary>
        /// Update UserCouponPackage
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Put
        /// 
        /// </remarks>
        /// <param name="um">UserCouponPackage Update Model</param>
        /// <returns></returns>
        /// <response code="200">Return Updated UserCouponPackage</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Update UserCouponPackage</response>
        [HttpPut]
        public IHttpActionResult Update(UserCouponPackageUM um)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

        /// <summary>
        /// Delete UserCouponPackage By Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Delete
        /// 
        /// </remarks>
        /// <param name="id">UserCouponPackage Id</param>
        /// <returns></returns>
        /// <response code="200">Return Empty</response>
        /// <response code="400">Invalid UserCouponPackage Id</response> 
        /// <response code="500">Fail to Delete UserCouponPackage</response>
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
