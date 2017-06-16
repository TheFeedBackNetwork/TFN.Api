using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Services.Posts
{
    public class PostService : IPostService
    {
        public IPostRepository PostRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }

        public PostService(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            PostRepository = postRepository;
            CommentRepository = commentRepository;
        }

        public async Task<CommentSummary> GetCommentScoreSummaryAsync(Guid commentId, int limit, string username)
        {
            return await CommentRepository.GetCommentScoreSummaryAsync(commentId, limit, username);
        }

        public async Task<PostSummary> GetPostLikeSummaryAsync(Guid postId, int limit, string username)
        {
            return await PostRepository.GetPostLikeSummaryAsync(postId, limit, username);
        }

        public async Task<IReadOnlyList<Post>> GetAllPostsAsync(int offset, int limit)
        {
            return await PostRepository.FindAll(offset, limit);
        }

        public async Task<Post> GetPostAsync(Guid postId)
        {
            return await PostRepository.Find(postId);
        }

        public async Task AddAsync(Post post)
        {
            await PostRepository.Add(post);
        }

        public async Task UpdateAsync(Post entity)
        {
            await PostRepository.Update(entity);
        }

        public async Task<IReadOnlyList<Like>> GetAllLikesAsync(Guid postId, int offset, int limit)
        {
            return await PostRepository.GetAllLikes(postId, offset, limit);
        }
        public async Task<Like> GetLikeAsync(Guid postId, Guid likeId)
        {
            return await PostRepository.GetLikeAsync(postId, likeId);
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsAsync(Guid postId, int offset, int limit)
        {
            return await CommentRepository.FindComments(postId, offset, limit);
        }

        public async Task<Comment> GetCommentAsync(Guid postId, Guid commentId)
        {
            //TODO Check for existing post or not?
            return await CommentRepository.Find(commentId);
        }

        public async Task<IReadOnlyList<Comment>> GetAllCommentsAsync(Guid postId)
        {
            return await CommentRepository.FindAllComments(postId);
        }

        public async Task<IReadOnlyList<Score>> GetAllScoresAsync(Guid commentId, int offset, int limit)
        {
            return await CommentRepository.FindAllScores(commentId, offset, limit);
        }

        public async Task<Score> GetScoreAsync(Guid commentId, Guid scoreId)
        {
            return await CommentRepository.GetAsync(commentId, scoreId);
        }

        public async Task UpdateAsync(Comment entity)
        {
            await CommentRepository.Update(entity);
        }

        public async Task AddAsync(Comment entity)
        {
            await CommentRepository.Add(entity);
        }

        public async Task AddAsync(Score entity)
        {
            await CommentRepository.AddAsync(entity);
        }

        public async Task DeletePostAsync(Guid postId)
        {
            await PostRepository.Delete(postId);
        }

        public async Task DeleteLikeAsync(Guid postId, Guid likeId)
        {
            await PostRepository.DeleteLike(postId, likeId);
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            await CommentRepository.Delete(commentId);
        }

        public async Task DeleteScoreAsync(Guid commentId, Guid scoreId)
        {
            await CommentRepository.DeleteAsync(commentId, scoreId);
        }


    }
}