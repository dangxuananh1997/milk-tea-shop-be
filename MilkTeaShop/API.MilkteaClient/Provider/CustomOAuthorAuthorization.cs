
namespace API.MilkteaClient.Provider
{
    using Core.AppService.Database.Identity;
    using Core.ObjectModel.Entity;
    using Core.ObjectModel.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Collections.Generic;
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

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var claims = context.Identity.Claims.Where(x => x.Type == ClaimTypes.Role);
            if (claims.ElementAt(0).Value.Equals(UserType.Administrator.ToString()))
            {
                return null;
            }
            context.AdditionalResponseParameters.Add("role", claims.ElementAt(0).Value);

            return base.TokenEndpointResponse(context);
        }
    }
}