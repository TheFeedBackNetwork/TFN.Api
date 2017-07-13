using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IScoreRepository : IAddableRepository<Score, Guid>
    {
        Task<IReadOnlyList<Score>> FindAll(Guid comentId);
        Task<Score> Find(Guid commentId, Guid scoreId);
        Task Delete(Guid commentId, Guid scoreId);
    }
}