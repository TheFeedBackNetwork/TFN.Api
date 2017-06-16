using System;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services.TransientUsers
{
    public class TransientUserService : ITransientUserService
    {
        public ITransientUserRepository TransientUserRepository { get; private set; }
        public IAccountEmailService AccountEmailService { get; private set; }
        public TransientUserService(ITransientUserRepository transientUserRepository,
            IAccountEmailService accountEmailService)
        {
            TransientUserRepository = transientUserRepository;
            AccountEmailService = accountEmailService;
        }
        public async Task CreateAsync(TransientUserAccount transientUserAccount)
        {
            if (transientUserAccount == null)
            {
                throw new ArgumentNullException(nameof(transientUserAccount));
            }

            await TransientUserRepository.Add(transientUserAccount);
            await AccountEmailService.SendVerificationEmailAsync(transientUserAccount.Email,transientUserAccount.Username, transientUserAccount.VerificationKey);

        }

        public async Task DeleteAsync(TransientUserAccount transientUserAccount)
        {
            await TransientUserRepository.Delete(transientUserAccount.Id);
        }

        public async Task<bool> EmailVerificationKeyExistsAsync(string emailVerificationKey)
        {
            var transientUser = await TransientUserRepository.FindByVerificationKey(emailVerificationKey);

            return transientUser != null;
        }

        public async Task<TransientUserAccount> GetByEmailVerificationKeyAsync(string emailVerificationKey)
        {
            return await TransientUserRepository.FindByVerificationKey(emailVerificationKey);
        }

        public async Task<TransientUserAccount> GetByEmailAsync(string email)
        {
            return await TransientUserRepository.FindByEmail(email);
        }

        public async Task<TransientUserAccount> GetByUsernameAsync(string username)
        {
            return await TransientUserRepository.FindByUsername(username);
        }
    }
}