using Core.ObjectModel.Identity;
using System.Threading.Tasks;

namespace Core.AppService.Database.Identity
{
    public interface IIdentityService
    {
        Task<SystemIdentityResult> GrantResourceOwnerCredentials(string userName, string password, string authenticateType);

        Task<SystemIdentityResult> Register(string username, string password);

        Task<SystemIdentityResult> ChangePassword(string userId, string newPassword);

        Task<bool> IsMatchPassword(string userId, string oldPassword);
    }
}
