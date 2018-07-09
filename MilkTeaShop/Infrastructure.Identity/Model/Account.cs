
namespace Infrastructure.Identity.Model
{
    using Core.ObjectModel.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Account : IdentityUser
    {
        public UserType UserType { get; set; }

        public Account()
        {
            this.UserType = UserType.Member;
        }

        public Account(string userName) : base(userName)
        {

        }
    }
}
