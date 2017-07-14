using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;


namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserAccountRepository : IAddableRepository<UserAccount, Guid>, IDeleteableRepository<UserAccount, Guid>, IUpdateableRepository<UserAccount,Guid>
    {
        Task<UserAccount> FindByUsername(string username);
        Task<UserAccount> FindByEmail(string email);
        Task<UserAccount> FindByChangePasswordKey(string changePasswordKey);
        Task<bool> ChangePasswordKeyExists(string changePasswordKey);
    }
}
