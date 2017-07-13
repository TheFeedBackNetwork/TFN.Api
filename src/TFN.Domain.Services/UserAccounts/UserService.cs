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
        public IUserAccountRepository UserAccountRepository { get; private set; }
        public IKeyService KeyService { get; private set; }
        public IAccountEmailService AccountEmailService { get; private set; }
        public ICreditService CreditService { get; private set; }
        public IPasswordService PasswordService { get; private set; }
        public UserService(IUserAccountRepository userAccountRepository, IKeyService keyService, IAccountEmailService accountEmailService, ICreditService creditService, IPasswordService passwordService)
        {
            UserAccountRepository = userAccountRepository;
            KeyService = keyService;
            AccountEmailService = accountEmailService;
            CreditService = creditService;
            PasswordService = passwordService;
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

            var hash = PasswordService.HashPassword(password);

            user.UpdateHashedPassword(hash);         
            
            await UserAccountRepository.Add(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await UserAccountRepository.Delete(id);
        }

        public async Task<UserAccount> GetAsync(Guid id)
        {
            var user = await UserAccountRepository.Find(id);

            return user;
        }

        public async Task<UserAccount> FindByUsername(string username)
        {
            var user = await UserAccountRepository.FindByUsername(username);

            return user;
        }

        public async Task<UserAccount> Find(string usernameOrEmail, string password)
        {
            UserAccount user = null;

            if (usernameOrEmail.IsEmail())
            {
                user = await UserAccountRepository.FindByEmail(usernameOrEmail);
            }
            else if(usernameOrEmail.IsValidUsername())
            {
                user = await UserAccountRepository.FindByUsername(usernameOrEmail);
            }

            if (PasswordService.VerifyHashedPassword(user.HashedPassword, password))
            {
                return user;
            }

            return null;

        }

        public async Task UpdateAsync(UserAccount entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            await UserAccountRepository.Update(entity);
        }
        

        public IReadOnlyList<Claim> GetClaims(UserAccount user)
        {
            var claims = user.GetClaims();

            return claims;
        }

        public async Task<bool> ValidateCredentials(string usernameOrEmail, string password)
        {
            
            var user = await Find(usernameOrEmail, password);

            return user != null;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var user = await UserAccountRepository.FindByEmail(email);

            return user != null;
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var user = await UserAccountRepository.FindByUsername(username);

            return user != null;
        }

        public async Task<UserAccount> FindByEmail(string email)
        {
            var user = await UserAccountRepository.FindByEmail(email);

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
        public async Task SendChangePasswordKey(UserAccount user)
        {
            var forgotPasswordKey = KeyService.GenerateUrlSafeUniqueKey();

            user.UpdateChangePasswordKey(forgotPasswordKey);
            await UserAccountRepository.Update(user);

            await AccountEmailService.SendChangePasswordEmail(user.Email, forgotPasswordKey);
        }
        public async Task<bool> ChangePasswordKeyExists(string changePasswordKey)
        {
            return await UserAccountRepository.ChangePasswordKeyExists(changePasswordKey);
        }

        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<UserAccount> FindByExternalProvider(string provider, string userId)
        {
            throw new NotImplementedException();
        }
        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<UserAccount> AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPassword(string changePasswordKey, string password)
        {
            var user = await UserAccountRepository.FindByChangePasswordKey(changePasswordKey);

            var newHash = PasswordService.HashPassword(password);
            user.UpdateHashedPassword(newHash);

            await UserAccountRepository.Update(user);
        }

        public async Task<UserAccount> FindByChangePasswordKey(string changePasswordKey)
        {
            return await UserAccountRepository.FindByChangePasswordKey(changePasswordKey);
        }

        public async Task<IReadOnlyList<Models.Entities.Credits>> SearchUsers(string searchToken, string continuationToken)
        {
            var credits = await CreditService.SearchUsers(searchToken, continuationToken);

            return credits;
        }

        public async Task<Models.Entities.Credits> FindCredits(string username)
        {
            var credits = await CreditService.GetByUsernameAsync(username);

            return credits;
        }
    }
}
