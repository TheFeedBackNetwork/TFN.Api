using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities.IdentityServer;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IProductApiResourceRepository : IAddableRepository<ProductApiResource, Guid>
    {
        Task<IReadOnlyCollection<ProductApiResource>> FindAll();
        Task<IReadOnlyCollection<ProductApiResource>> FindAll(IReadOnlyCollection<string> scopeNames);
        Task<ProductApiResource> Find(string scopeName);
    }
}