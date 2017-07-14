using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IScoreRepository : IAddableRepository<Score, Guid>, IDeleteableRepository<Score,Guid>
    {
        Task<IReadOnlyList<Score>> FindAllPaginated(Guid comentId, string continuationToken);
        Task<int> Count(Guid commentId); 
    }
}