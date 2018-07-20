using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.AppService.Pagination;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using Core.ObjectModel.Pagination;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace API.MilkteaAdmin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IPagination _pagination;

        public UsersController(IUserService userService, IPagination pagination)
        {
            this._userService = userService;
            this._pagination = pagination;
        }

        /// <summary>
        /// Get list Users Paged
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Get
        /// 
        /// </remarks>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="searchValue">Search Value</param>
        /// <returns></returns>
        /// <response code="200">Return Users</response>
        /// <response code="400">Invalid Page Index</response> 
        /// <response code="500">Fail to Retrieve Users</response>
        [HttpGet]
        public IHttpActionResult Get(int pageIndex, string searchValue)
        {
            if (pageIndex <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_PAGEINDEX);
            }

            try
            {
                List<User> users;
                if (String.IsNullOrEmpty(searchValue))
                {
                    // GET ALL
                    users = _userService.GetAllUser().ToList();
                }
                else
                {
                    // GET SEARCH RESULT
                    users = _userService.GetAllUser().Where(p => p.FullName.Contains(searchValue)).ToList();
                }

                List<UserVM> userVMs = AutoMapper.Mapper.Map<List<User>, List<UserVM>>(users);
                Pager<UserVM> result = _pagination.ToPagedList<UserVM>(pageIndex, ConstantDataManager.PAGESIZE, userVMs);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Get
        /// 
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        /// <response code="200">Return User</response>
        /// <response code="400">Invalid Id</response> 
        /// <response code="500">Fail to Retrieve User</response>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }
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

        /// <summary>
        /// Create User
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Post
        /// 
        /// </remarks>
        /// <param name="cm">User Create Model</param>
        /// <returns></returns>
        /// <response code="200">Return Created User</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Create User</response>
        [HttpPost]
        public IHttpActionResult Create(UserCM cm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

        /// <summary>
        /// Update User
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Put
        /// 
        /// </remarks>
        /// <param name="um">User Update Model</param>
        /// <returns></returns>
        /// <response code="200">Return Updated User</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Update User</response>
        [HttpPut]
        public IHttpActionResult Update(UserUM um)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                User updateUser = AutoMapper.Mapper.Map<UserUM, User>(um);
                User oldUser = _userService.GetUserAsNoTracking(u => u.Id == um.Id);

                if (!um.Avatar.Contains("/Media/User/"))
                {
                    // DELETE OLD AVATAR
                    // physical path to folder contain user avatar
                    string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/Media/User/");
                    // physical path to this user avatar
                    string physicalPath = null;
                    if (!String.IsNullOrEmpty(oldUser.Avatar))
                    {
                        physicalPath = folderPath + oldUser.Avatar.Substring(oldUser.Avatar.LastIndexOf("/") + 1);
                    }
                    // delete old picture
                    if (File.Exists(physicalPath))
                    {
                        File.Delete(physicalPath);
                    }


                    // MAPPING NEW PICTURE
                    // new Guid
                    Guid newGuid = Guid.NewGuid();
                    // image stream
                    var bytes = Convert.FromBase64String(um.Avatar);
                    // save image to server
                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                        image.Save(folderPath + newGuid + ".jpg");
                    }
                    updateUser.Avatar = "/Media/User/" + newGuid + ".jpg";
                }
                else
                {
                    updateUser.Avatar = oldUser.Avatar;
                }

                // UPDATE
                updateUser.Username = oldUser.Username;
                _userService.UpdateUser(updateUser);
                _userService.SaveUserChanges();

                UserVM result = AutoMapper.Mapper.Map<User, UserVM>(updateUser);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// Delete
        /// 
        /// </remarks>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        /// <response code="200">Return Empty</response>
        /// <response code="400">Invalid User Id</response> 
        /// <response code="500">Fail to Delete User</response>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }
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
