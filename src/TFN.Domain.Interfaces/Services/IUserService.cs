using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface IUserService
    {  
        Task<UserAccount> AutoProvisionUserAsync(string provider, string userId, List<Claim> claims);
        Task<bool> ValidateCredentialsAsync(string usernameOrEmail, string password);
        bool ValidateUsernameCharacterSafety(string password);
        Task<UserAccount> FindByExternalProviderAsync(string provider, string userId);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByUsername(string username);
        Task CreateAsync(UserAccount user, string password);
        IReadOnlyList<Claim> GetClaims(UserAccount user);
        Task SendChangePasswordKeyAsync(UserAccount user);
        Task<UserAccount> GetByUsernameAsync(string username);
        Task<UserAccount> GetByEmailAsync(string email);
        Task<UserAccount> GetAsync(string usernameOrEmail, string password);
        Task<UserAccount> GetByChangePasswordKey(string changePasswordKey);
        Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey);
        Task UpdateUserPasswordAsync(string changePasswordKey, string password);
        Task<IReadOnlyList<Credits>> SearchUsers(string searchToken,int offset,int limit);
        Task<Credits> GetCredits(string username);
    }
}
