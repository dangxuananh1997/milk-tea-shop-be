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
    public class UserCouponPackagesController : ApiController
    {
        private readonly IUserCouponPackageService _userCouponPackageService;

        public UserCouponPackagesController(IUserCouponPackageService userCouponPackageService)
        {
            this._userCouponPackageService = userCouponPackageService;
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
