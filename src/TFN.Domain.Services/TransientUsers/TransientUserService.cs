using System;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services.TransientUsers
{
    public class TransientUserService : ITransientUserService
    {
        public ITransientUserAccountRepository TransientUserAccountRepository { get; private set; }
        public IAccountEmailService AccountEmailService { get; private set; }
        public TransientUserService(ITransientUserAccountRepository transientUserAccountRepository,
            IAccountEmailService accountEmailService)
        {
            TransientUserAccountRepository = transientUserAccountRepository;
            AccountEmailService = accountEmailService;
        }
        public async Task CreateAsync(TransientUserAccount transientUserAccount)
        {
            if (transientUserAccount == null)
            {
                throw new ArgumentNullException(nameof(transientUserAccount));
            }

            await TransientUserAccountRepository.Add(transientUserAccount);
            await AccountEmailService.SendVerificationEmailAsync(transientUserAccount.Email,transientUserAccount.Username, transientUserAccount.VerificationKey);

        }

        public async Task DeleteAsync(TransientUserAccount transientUserAccount)
        {
            await TransientUserAccountRepository.Delete(transientUserAccount.Id);
        }

        public async Task<bool> EmailVerificationKeyExistsAsync(string emailVerificationKey)
        {
            var transientUser = await TransientUserAccountRepository.FindByVerificationKey(emailVerificationKey);

            return transientUser != null;
        }

        public async Task<TransientUserAccount> GetByEmailVerificationKeyAsync(string emailVerificationKey)
        {
            return await TransientUserAccountRepository.FindByVerificationKey(emailVerificationKey);
        }

        public async Task<TransientUserAccount> GetByEmailAsync(string email)
        {
            return await TransientUserAccountRepository.FindByEmail(email);
        }

        public async Task<TransientUserAccount> GetByUsernameAsync(string username)
        {
            return await TransientUserAccountRepository.FindByUsername(username);
        }
    }
}