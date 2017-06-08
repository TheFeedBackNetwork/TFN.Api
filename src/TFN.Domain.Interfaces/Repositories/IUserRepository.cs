using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;


namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IAddableRepository<UserAccount, Guid>, IDeleteableRepository<UserAccount, Guid>, IUpdateableRepository<UserAccount,Guid>
    {
        Task<UserAccount> GetByUsernameAsync(string username);
        Task<UserAccount> GetByEmailAsync(string email);
        Task<UserAccount> GetByChangePasswordKey(string changePasswordKey);
        Task<UserAccount> GetByUsernameAsync(string username,string password);
        Task<UserAccount> GetByEmailAsync(string email, string password);
        Task Add(UserAccount entity, string password);
        Task UpdateChangePasswordKeyAsync(UserAccount user, string changePasswordKey);
        Task UpdateUserPasswordAsync(UserAccount user, string password);
        Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey);
    }
}
