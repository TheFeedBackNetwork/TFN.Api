using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ILikeRepository : IAddableRepository<Like,Guid>, IDeleteableRepository<Like,Guid>
    {
        Task<IReadOnlyList<Like>> FindLikesPaginated(Guid postId, string continuationToken);
        Task<bool> Exists(Guid postId, Guid userId);
        Task<Like> Find(Guid postId, Guid userId);
        Task<int> Count(Guid postId);
    }
}