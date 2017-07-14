using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ITransientUserAccountRepository : IAddableRepository<TransientUserAccount, Guid>, IDeleteableRepository<TransientUserAccount, Guid>, IUpdateableRepository<TransientUserAccount, Guid>
    {
        Task<TransientUserAccount> FindByEmail(string email);
        Task<TransientUserAccount> FindByVerificationKey(string verificationKey);
        Task<TransientUserAccount> FindByUsername(string username);
    }
}