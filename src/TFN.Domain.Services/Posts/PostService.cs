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

        public async Task<CommentSummary> FindCommentScoreSummary(Guid commentId, int limit, string username)
        {
            return await CommentRepository.FindCommentScoreSummary(commentId, limit, username);
        }

        public async Task<PostSummary> FindPostLikeSummary(Guid postId, int limit, string username)
        {
            return await PostRepository.FindPostLikeSummary(postId, limit, username);
        }

        public async Task<IReadOnlyList<Post>> FindAllPostsPaginated(string continuationToken)
        {
            return await PostRepository.FindAllPostsPaginated(continuationToken);
        }

        public async Task<Post> FindPost(Guid postId)
        {
            return await PostRepository.Find(postId);
        }

        public async Task Add(Post post)
        {
            await PostRepository.Add(post);
        }

        public async Task Update(Post entity)
        {
            await PostRepository.Update(entity);
        }

        public async Task<IReadOnlyList<Like>> FindAllLikes(Guid postId, string continuationToken)
        {
            return await PostRepository.FindAllLikes(postId, continuationToken);
        }
        public async Task<Like> FindLike(Guid postId, Guid likeId)
        {
            return await PostRepository.GetLikeAsync(postId, likeId);
        }

        public async Task<IReadOnlyList<Comment>> FindComments(Guid postId, string continuationToken)
        {
            throw new NotImplementedException();
            //return await CommentRepository.FindCommentsPaginated(postId, offset);
        }

        public async Task<Comment> FindComment(Guid postId, Guid commentId)
        {
            //TODO Check for existing post or not?
            return await CommentRepository.Find(commentId);
        }

        public async Task<IReadOnlyList<Comment>> FindAllComments(Guid postId)
        {
            throw new NotImplementedException();
            //return await CommentRepository.FindAllComments(postId);
        }

        public async Task<IReadOnlyList<Score>> AllScores(Guid commentId, string continuationToken)
        {
            return await CommentRepository.FindAllScores(commentId, continuationToken);
        }

        public async Task<Score> FindScore(Guid commentId, Guid scoreId)
        {
            return await CommentRepository.Find(commentId, scoreId);
        }

        public async Task Update(Comment entity)
        {
            await CommentRepository.Update(entity);
        }

        public async Task Add(Comment entity)
        {
            await CommentRepository.Add(entity);
        }

        public async Task Add(Score entity)
        {
            await CommentRepository.Add(entity);
        }

        public async Task DeletePost(Guid postId)
        {
            await PostRepository.Delete(postId);
        }

        public async Task DeleteLike(Guid postId, Guid likeId)
        {
            await PostRepository.DeleteLike(postId, likeId);
        }

        public async Task DeleteComment(Guid commentId)
        {
            await CommentRepository.Delete(commentId);
        }

        public async Task DeleteScore(Guid commentId, Guid scoreId)
        {
            await CommentRepository.Delete(commentId, scoreId);
        }


    }
}