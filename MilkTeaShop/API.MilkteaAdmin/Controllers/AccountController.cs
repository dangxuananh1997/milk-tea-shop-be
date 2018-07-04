using API.MilkteaAdmin.Models;
using Core.AppService.Database.Identity;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody]RegisterBindingModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SystemIdentityResult result = await this._identityService.Register(account.Email, account.Password);

            if (result.IsError)
            {
                this.AddErrors(result.Errors);

                return BadRequest(ModelState);
            }
            else
            {
                return Ok();
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
