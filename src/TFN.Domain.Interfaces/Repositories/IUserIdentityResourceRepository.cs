using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities.IdentityServer;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserIdentityResourceRepository : IAddableRepository<UserIdentityResource, Guid>
    {
        Task<IReadOnlyCollection<UserIdentityResource>> FindAll();
        Task<IReadOnlyCollection<UserIdentityResource>> FindAll(IReadOnlyCollection<string> scopeNames);
    }
}