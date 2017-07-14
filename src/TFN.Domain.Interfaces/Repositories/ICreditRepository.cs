using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ICreditRepository : IAddableRepository<Credits, Guid>, IUpdateableRepository<Credits, Guid>
    {
        Task<Credits> FindByUsername(string username);
        Task<Credits> FindByUserId(Guid userId);
        Task<IReadOnlyList<Credits>> FindHighestCredits(string continuationToken);
        Task<IReadOnlyList<Credits>> FindUsers(string searchToken, string continuationToken);
    }
}