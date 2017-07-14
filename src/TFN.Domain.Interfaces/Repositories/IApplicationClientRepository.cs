using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities.IdentityServer;

namespace TFN.Domain.Interfaces.Repositories
{
                                                                                            //REMOVE THIS IS USED FOR SEEDDATAONLY
    public interface IApplicationClientRepository : IRepository<ApplicationClient, Guid>, IAddableRepository<ApplicationClient, Guid>
    {
        Task<ApplicationClient> Find(string clientId);
        Task<IReadOnlyCollection<string>> FindAllAllowedCorsOrigins();
    }
}