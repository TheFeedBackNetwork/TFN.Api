using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ICommentRepository : IAddableRepository<Comment, Guid>, IUpdateableRepository<Comment, Guid>, IDeleteableRepository<Comment, Guid>
    {
        Task<IReadOnlyList<Comment>> FindCommentsPaginated(Guid postId, string continuationToken);
        Task<int> Count(Guid postId);

        //Score
        Task<IReadOnlyList<Score>> FindAllScores(Guid comentId, string continuationToken);
        Task<Score> Find( Guid commentId, Guid scoreId);
        Task Add(Score entity);
        Task Delete(Guid commentId, Guid scoreId);
        Task<CommentSummary> FindCommentScoreSummary(Guid commentId, int limit, string username);
    }
}