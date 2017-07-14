using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Interfaces.Services
{
    public interface IPostService
    {
        Task<CommentSummary>  FindCommentScoreSummary(Guid commentId, int limit, string username);
        Task<PostSummary> FindPostLikeSummary(Guid postId, int limit, string username);
    }
}