using System;
using System.Threading.Tasks;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Interfaces.Services
{
    public interface IPostService
    {
        Task<CommentSummary> FindCommentScoreSummary(Guid commentId, Guid userId);
        Task<PostSummary> FindPostLikeSummary(Guid postId, Guid userId);
    }
}