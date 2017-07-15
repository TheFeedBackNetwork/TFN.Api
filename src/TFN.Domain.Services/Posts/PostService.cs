using System;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Services.Posts
{
    public class PostService : IPostService
    {
        public IPostRepository PostRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public IScoreRepository ScoreRepository { get; private set; }
        public ILikeRepository LikeRepository { get; private set; }
        public IListenRepository ListenRepository { get; private set; }

        public PostService(IPostRepository postRepository, ICommentRepository commentRepository,
            IScoreRepository scoreRepository, ILikeRepository likeRepository,
            IListenRepository listenRepository)
        {
            PostRepository = postRepository;
            CommentRepository = commentRepository;
            ScoreRepository = scoreRepository;
            LikeRepository = likeRepository;
            ListenRepository = listenRepository;
        }

        public async Task<CommentSummary> FindCommentScoreSummary(Guid commentId, Guid userId)
        {
            var hasScored = await ScoreRepository.Exists(commentId, userId);
            var count = await ScoreRepository.Count(commentId);
            var someScores = await ScoreRepository.FindScoresPaginated(commentId, null);

            var summary = CommentSummary.From(commentId, someScores, count, hasScored);

            return summary;
        }

        public async Task<PostSummary> FindPostLikeSummary(Guid postId, Guid userId)
        {
            var hasLiked = await LikeRepository.Exists(postId, userId);
            var listens = await ListenRepository.Count(postId);
            var count = await LikeRepository.Count(postId);
            var someLikes = await LikeRepository.FindLikesPaginated(postId, null);

            var summary = PostSummary.From(postId, someLikes,listens, count, hasLiked);

            return summary;
        }
        
    }
}