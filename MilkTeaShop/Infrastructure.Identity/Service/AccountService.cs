
namespace Infrastructure.Identity.Service
{
    using Infrastructure.Identity.Model;
    using Microsoft.AspNet.Identity;

    public class AccountService : UserManager<Account>
    {
        public AccountService(IUserStore<Account> store) : base(store)
        {

        }
    }
}
