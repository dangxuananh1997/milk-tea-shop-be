using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.AppService.Database.Identity;
using Core.ObjectModel.Entity;
using Core.ObjectModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.MilkteaAdmin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : ApiController
    {
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;

        /// <summary>
        /// AccountController Constructor
        /// </summary>
        /// <param name="identityService"></param>
        /// <param name="userService"></param>
        public AccountController(IIdentityService identityService, IUserService userService)
        {
            this._identityService = identityService;
            this._userService = userService;
        }

        /// <summary>
        /// Register an account
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="account">RegisterBindingModel</param>
        /// <returns>BadRequest or Ok</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody]RegisterModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create User account in AspNetUsers
            SystemIdentityResult result = await this._identityService.Register(account.Username, account.Password);


            if (result.IsError)
            {
                this.AddErrors(result.Errors);

                return BadRequest(ModelState);
            }
            else
            {
                // Create user info in User
                User user = AutoMapper.Mapper.Map<RegisterModel, User>(account);
                _userService.CreateUser(user);
                _userService.SaveUserChanges();
                return Ok(user);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> ChangePassword([FromBody]AccountModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = ClaimsPrincipal.Current?.FindFirst(ClaimTypes.NameIdentifier).Value ?? string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                bool isMatchPassword = await this._identityService.IsMatchPassword(userId, account.OldPassword);

                if (isMatchPassword)
                {
                    SystemIdentityResult result = await this._identityService.ChangePassword(userId, account.NewPassword);
                    if (result.IsError)
                    {
                        this.AddErrors(result.Errors);
                    }
                    else
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        private void AddErrors(IList<string> errors)
        {
            for (int i = 0; i < errors.Count; i++)
            {
                ModelState.AddModelError($"{i}", errors[i]);
            }
        }
    }
}
