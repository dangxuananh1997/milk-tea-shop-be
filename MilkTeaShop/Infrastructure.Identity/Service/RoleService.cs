
namespace Infrastructure.Identity.Service
{
    using Infrastructure.Identity.Model;
    using Microsoft.AspNet.Identity;

    public class RoleService : RoleManager<Role>
    {
        public RoleService(IRoleStore<Role, string> store) : base(store)
        {

        }
    }
}
