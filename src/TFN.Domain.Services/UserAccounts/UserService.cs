using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Extensions;

namespace TFN.Domain.Services.UserAccounts
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; private set; }
        public IKeyService KeyService { get; private set; }
        public IAccountEmailService AccountEmailService { get; private set; }
        public ICreditService CreditService { get; private set; }
        public UserService(IUserRepository userRepository, IKeyService keyService, IAccountEmailService accountEmailService, ICreditService creditService)
        {
            UserRepository = userRepository;
            KeyService = keyService;
            AccountEmailService = accountEmailService;
            CreditService = creditService;
        }

        public async Task CreateAsync(UserAccount user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(user)}");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException($"{nameof(password)}");
            }

            var credit = new Models.Entities.Credits(user.Id,user.Username);
            await CreditService.AddAsync(credit);
            await UserRepository.Add(user, password);
        }

        public async Task DeleteAsync(Guid id)
        {
            await UserRepository.Delete(id);
        }

        public async Task<UserAccount> GetAsync(Guid id)
        {
            var user = await UserRepository.Find(id);

            return user;
        }

        public async Task<UserAccount> GetByUsernameAsync(string username)
        {
            var user = await UserRepository.GetByUsernameAsync(username);

            return user;
        }

        public async Task<UserAccount> GetAsync(string usernameOrEmail, string password)
        {
            UserAccount user = null;

            if (usernameOrEmail.IsEmail())
            {
                user = await UserRepository.GetByEmailAsync(usernameOrEmail, password);
            }
            else if(usernameOrEmail.IsValidUsername())
            {
                user = await UserRepository.GetByUsernameAsync(usernameOrEmail, password);
            }

            return user;
        }

        public async Task UpdateAsync(UserAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            await UserRepository.Update(entity);
        }
        

        public IReadOnlyList<Claim> GetClaims(UserAccount user)
        {
            var claims = user.GetClaims();

            return claims;
        }

        public async Task<bool> ValidateCredentialsAsync(string usernameOrEmail, string password)
        {
            
            var user = await GetAsync(usernameOrEmail, password);

            return user != null;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var user = await UserRepository.GetByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var user = await UserRepository.GetByUsernameAsync(username);

            return user != null;
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            var user = await UserRepository.GetByEmailAsync(email);

            return user;
        }

        public bool ValidateUsernameCharacterSafety(string username)
        {
            const string invalidUrlCharacters = "!&$+,/.;=?@#'<>[]{}()|\\^%\"*";

            if (username == null)
            {
                return false;
            }

            if (invalidUrlCharacters.Any(c => username.Contains(c.ToString())))
            {
                return false;
            }

            return true;
        }
        public async Task SendChangePasswordKeyAsync(UserAccount user)
        {
            var forgotPasswordKey = KeyService.GenerateUrlSafeUniqueKey();
            await UserRepository.UpdateChangePasswordKeyAsync(user, forgotPasswordKey);
            await AccountEmailService.SendChangePasswordEmailAsync(user.Email, forgotPasswordKey);
        }
        public async Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey)
        {
            return await UserRepository.ChangePasswordKeyExistsAsync(changePasswordKey);
        }

        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<UserAccount> FindByExternalProviderAsync(string provider, string userId)
        {
            throw new NotImplementedException();
        }
        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<UserAccount> AutoProvisionUserAsync(string provider, string userId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPasswordAsync(string changePasswordKey, string password)
        {
            var user = await UserRepository.GetByChangePasswordKey(changePasswordKey);
            await UserRepository.UpdateUserPasswordAsync(user, password);
        }

        public async Task<UserAccount> GetByChangePasswordKey(string changePasswordKey)
        {
            return await UserRepository.GetByChangePasswordKey(changePasswordKey);
        }

        public async Task<IReadOnlyList<Models.Entities.Credits>> SearchUsers(string searchToken, int offset, int limit)
        {
            var credits = await CreditService.SearchUsers(searchToken, offset, limit);

            return credits;
        }

        public async Task<Models.Entities.Credits> GetCredits(string username)
        {
            var credits = await CreditService.GetByUsernameAsync(username);

            return credits;
        }
    }
}
