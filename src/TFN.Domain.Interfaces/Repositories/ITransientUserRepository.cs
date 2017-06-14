using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ITransientUserRepository : IAddableRepository<TransientUserAccount, Guid>, IDeleteableRepository<TransientUserAccount, Guid>, IUpdateableRepository<TransientUserAccount, Guid>
    {
        Task<TransientUserAccount> GetByEmailAsync(string email);
        Task<TransientUserAccount> GetByEmailVerificationKeyAsync(string emailVerificationKey);
        Task<TransientUserAccount> GetByUsernameAsync(string username);
    }
}