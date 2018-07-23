using API.MilkteaClient.Models;
using Core.AppService.Business;
using Core.ObjectModel.Entity;
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace API.MilkteaClient.Controllers
{
    [Authorize(Roles = "Member")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly int CURRENT_USER_ID;

        public UsersController(IUserService userService)
        {
            this._userService = userService;

            // Get the current requesting user
            string username = HttpContext.Current.User.Identity.GetUserName();
            CURRENT_USER_ID = _userService.GetUserAsNoTracking(u => u.Username.Equals(username)).Id;
        }


        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                UserVM result = AutoMapper.Mapper.Map<User, UserVM>
                    (_userService.GetUser(CURRENT_USER_ID));

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
                User oldUser = _userService.GetUserAsNoTracking(u => u.Id == CURRENT_USER_ID);

                if (!um.Avatar.Contains("/Media/User/") && !string.IsNullOrEmpty(um.Avatar))
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
                updateUser.Id = oldUser.Id;
                updateUser.Username = oldUser.Username;

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
