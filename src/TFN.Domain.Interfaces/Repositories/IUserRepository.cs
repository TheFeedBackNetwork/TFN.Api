﻿using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;


namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IAddableRepository<User, Guid>, IDeleteableRepository<User, Guid>, IUpdateableRepository<User,Guid>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByChangePasswordKey(string changePasswordKey);
        Task<User> GetByUsernameAsync(string username,string password);
        Task<User> GetByEmailAsync(string email, string password);
        Task Add(User entity, string password);
        Task UpdateChangePasswordKeyAsync(User user, string changePasswordKey);
        Task UpdateUserPasswordAsync(User user, string password);
        Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey);
    }
}
