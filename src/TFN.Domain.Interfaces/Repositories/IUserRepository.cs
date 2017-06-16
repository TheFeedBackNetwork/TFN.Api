using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;


namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IAddableRepository<UserAccount, Guid>, IDeleteableRepository<UserAccount, Guid>, IUpdateableRepository<UserAccount,Guid>
    {
        Task<UserAccount> FindByUsername(string username);
        Task<UserAccount> FindByEmail(string email);
        Task<UserAccount> FindByChangePasswordKey(string changePasswordKey);
        Task<UserAccount> FindByUsername(string username,string password);
        Task<UserAccount> FindByEmail(string email, string password);
        Task Add(UserAccount entity, string password);
        Task UpdateChangePasswordKey(UserAccount user, string changePasswordKey);
        Task UpdateUserPassword(UserAccount user, string password);
        Task<bool> ChangePasswordKeyExists(string changePasswordKey);
    }
}
