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
        Task<IReadOnlyList<Post>> FindAllPostsPaginated(string continuationToken);
        Task<Post> FindPost(Guid postId);
        Task Add(Post post);
        Task Update(Post entity);
        Task<IReadOnlyList<Like>> FindAllLikes(Guid postId, string continuationToken);
        Task<IReadOnlyList<Comment>> FindComments(Guid postId, string continuationToken);
        Task<Comment> FindComment(Guid postId, Guid commentId);
        Task<IReadOnlyList<Comment>> FindAllComments(Guid postId);
        Task<IReadOnlyList<Score>> AllScores(Guid commentId, string continuationToken);
        Task<Score> FindScore(Guid commentId, Guid scoreId);
        Task<Like> FindLike(Guid postId, Guid likeId);
        Task Update(Comment entity);
        Task Add(Comment entity);
        Task Add(Score entity);
        Task DeletePost(Guid postId);
        Task DeleteLike(Guid postId, Guid likeId);
        Task DeleteComment(Guid commentId);
        Task DeleteScore(Guid commentId, Guid scoreId);
    }
}