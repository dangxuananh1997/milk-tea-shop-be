﻿
namespace API.MilkteaAdmin.Provider
{
    using Core.AppService.Database.Identity;
    using Core.ObjectModel.Identity;
    using Microsoft.Owin.Security.OAuth;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class CustomOAuthorAuthorization : OAuthAuthorizationServerProvider
    {
        private IIdentityService _identityService;

        public CustomOAuthorAuthorization(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            SystemIdentityResult result = await _identityService.GrantResourceOwnerCredentials(context.UserName, context.Password, context.Options.AuthenticationType);

            if (result.IsError)
            {
                context.SetError("invalid_grant", result.Errors.FirstOrDefault());
                return;
            }

            ClaimsIdentity claimIdentity = result.Data as ClaimsIdentity;
            context.Validated(claimIdentity);
        }
    }
}