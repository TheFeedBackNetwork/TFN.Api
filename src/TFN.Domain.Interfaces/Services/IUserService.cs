using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface IUserService
    {  
        Task<UserAccount> AutoProvisionUser(string provider, string userId, List<Claim> claims);
        Task<bool> ValidateCredentials(string usernameOrEmail, string password);
        bool ValidateUsernameCharacterSafety(string password);
        Task<UserAccount> FindByExternalProvider(string provider, string userId);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByUsername(string username);
        Task Create(UserAccount user, string password);
        Task SendChangePasswordKey(UserAccount user);
        Task<UserAccount> FindByUsername(string username);
        Task<UserAccount> FindByEmail(string email);
        Task<UserAccount> Find(string usernameOrEmail, string password);
        Task<UserAccount> FindByChangePasswordKey(string changePasswordKey);
        Task<bool> ChangePasswordKeyExists(string changePasswordKey);
        Task UpdateUserPassword(string changePasswordKey, string password);
        Task<IReadOnlyList<Credits>> SearchUsers(string searchToken, string continuationToken);
        Task<Credits> FindCredits(string username);
    }
}
