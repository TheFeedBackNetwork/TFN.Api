using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities.IdentityServer;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IApplicationClientRepository : IRepository<ApplicationClient, Guid>
    {
        Task<ApplicationClient> Find(string clientId);
        Task<IReadOnlyCollection<string>> FindAllAllowedCorsOrigins();
    }
}