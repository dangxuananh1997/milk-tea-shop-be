
namespace Infrastructure.Identity
{
    using Core.AppService.Database.Identity;
    using Infrastructure.Identity.Database;

    public class IdentityProvider : IIdentityProvider
    {
        public object GetContext()
        {
            return new IdentityContext();
        }
    }
}
