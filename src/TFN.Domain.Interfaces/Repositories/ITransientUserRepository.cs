﻿using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ITransientUserRepository : IAddableRepository<TransientUser, Guid>, IDeleteableRepository<TransientUser, Guid>, IUpdateableRepository<TransientUser, Guid>
    {
        Task<TransientUser> GetByEmailAsync(string email);
        Task<TransientUser> GetByEmailVerificationKeyAsync(string emailVerificationKey);
        Task<TransientUser> GetByUsernameAsync(string username);
    }
}