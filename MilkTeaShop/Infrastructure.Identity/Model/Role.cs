
namespace Infrastructure.Identity.Model
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Role : IdentityRole
    {
        public Role()
        {

        }

        public Role(string roleName) : base(roleName)
        {

        }
    }
}
