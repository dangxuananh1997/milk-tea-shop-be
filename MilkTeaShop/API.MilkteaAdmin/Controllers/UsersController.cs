using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API.MilkteaAdmin.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                List<UserVM> result = AutoMapper.Mapper.Map<List<User>, List<UserVM>>
                    (_userService.GetAllUser().ToList());

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
                UserVM result = AutoMapper.Mapper.Map<User, UserVM>
                    (_userService.GetUser(id));

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(UserCM cm)
        {
            try
            {
                User model = AutoMapper.Mapper.Map<UserCM, User>(cm);
                _userService.CreateUser(model);
                _userService.SaveUserChanges();

                UserVM result = AutoMapper.Mapper.Map<User, UserVM>(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(UserUM um)
        {
            try
            {
                User model = AutoMapper.Mapper.Map<UserUM, User>(um);
                _userService.UpdateUser(model);
                _userService.SaveUserChanges();

                UserVM result = AutoMapper.Mapper.Map<User, UserVM>(model);
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
                _userService.DeleteUser(id);
                _userService.SaveUserChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
