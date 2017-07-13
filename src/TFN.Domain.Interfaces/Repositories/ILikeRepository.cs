using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ILikeRepository : IAddableRepository<Like,Guid>, IDeleteableRepository<Like,Guid>
    {
        Task<IReadOnlyList<Like>> FindAll(Guid postId, string continuationToken);
        Task Delete(Guid postId, Guid likeId);
        Task<Like> Find(Guid postId, Guid likeId);
    }
}