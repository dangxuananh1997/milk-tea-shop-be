using API.MilkteaClient.Models;
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
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MilkteaClient.Controllers
{
    [Authorize(Roles = "Member")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IPagination _pagination;

        public UsersController(IUserService userService, IPagination pagination)
        {
            this._userService = userService;
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

        [HttpPut]
        public IHttpActionResult Update(UserUM um)
        {
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
    }
}
