using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IAddableRepository<Post,Guid> , IUpdateableRepository<Post,Guid>, IDeleteableRepository<Post,Guid>
    {
        Task<IReadOnlyList<Post>> FindAll(int offset, int limit);


        //Like
        Task<PostSummary> FindPostLikeSummary(Guid postId, int limit, string username);
        Task<IReadOnlyList<Like>> FindAllLikes(Guid postId, int offset, int limit);
        Task DeleteLike(Guid postId, Guid likeId);
        Task<Like> GetLikeAsync(Guid postId, Guid likeId);
    }
}
