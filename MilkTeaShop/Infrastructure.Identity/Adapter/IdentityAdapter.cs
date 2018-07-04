﻿
namespace Infrastructure.Identity.Adapter
{
    using Core.AppService.Database.Identity;
    using Core.ObjectModel.Identity;
    using Infrastructure.Identity.Model;
    using Infrastructure.Identity.Service;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class IdentityAdapter : IIdentityService
    {
        private readonly RoleService _roleService;
        private readonly AccountService _accountService;

        public IdentityAdapter(IIdentityProvider identityContext)
        {
            DbContext dbContext = identityContext.GetContext() as DbContext;
            if (dbContext == null)
            {
                throw new ArgumentException();
            }
            this._roleService = new RoleService(new RoleStore<Role>(dbContext));
            this._accountService = new AccountService(new UserStore<Account>(dbContext));
        }

        public async Task<SystemIdentityResult> ChangePassword(string userId, string newPassword)
        {
            SystemIdentityResult result = new SystemIdentityResult();

            IdentityResult user = await this._accountService.ResetPasswordAsync
                (userId, await this._accountService.GeneratePasswordResetTokenAsync(userId), newPassword);

            if (!user.Succeeded)
            {
                result.Errors.AddRange(user.Errors);
            }
            return result;
        }

        public async Task<SystemIdentityResult> GrantResourceOwnerCredentials(string userName, string password, string authenticateType)
        {
            SystemIdentityResult result = new SystemIdentityResult();
            Account user = await this._accountService.FindAsync(userName, password);
            if (user == null)
            {
                result.Errors.Add("Invalid Username or Password.");
            }
            else
            {
                if (user.LockoutEnabled)
                {
                    result.Errors.Add("Unable to login, please contact your administrator for more information");
                }
                else
                {
                    ClaimsIdentity claimsIdentity = await this._accountService.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

                    result.Data = claimsIdentity;
                }
            }
            return result;
        }

        public async Task<SystemIdentityResult> Register(string username, string password)
        {
            SystemIdentityResult result = new SystemIdentityResult();

            IdentityResult user = await this._accountService.CreateAsync(new Account { UserName = username }, password);

            if (!user.Succeeded)
            {
                result.Errors.AddRange(user.Errors);
            }
            return result;
        }

        public async Task<bool> IsMatchPassword(string userId, string oldPassword)
        {
            Account user = await this._accountService.FindByIdAsync(userId);
            return await this._accountService.FindAsync(user.UserName, oldPassword) != null;
        }
    }
}
