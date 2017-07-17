using System;
using System.Collections.Generic;
using System.Linq;
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
            var count = await ScoreRepository.Count(commentId);

            var viewUserScore = await ScoreRepository.Find(commentId, userId);
            var hasScored = viewUserScore != null;

            var someScores = await ScoreRepository.FindScoresPaginated(commentId, null);
            var list = someScores.Where(x => x.UserId != userId).ToList();

            if (hasScored)
            {
                list.Add(viewUserScore);
            }

            var summary = CommentSummary.From(commentId, list, count, hasScored);

            return summary;
        }

        public async Task<PostSummary> FindPostLikeSummary(Guid postId, Guid userId)
        {
            var viewUserLiked = await LikeRepository.Find(postId, userId);
            var hasLiked = viewUserLiked != null;

            var listens = await ListenRepository.Count(postId);
            var count = await LikeRepository.Count(postId);
            var someLikes = await LikeRepository.FindLikesPaginated(postId, null);
            var list = someLikes.Where(x => x.UserId != userId).ToList();

            if (hasLiked)
            {
                list.Add(viewUserLiked);
            }

            var summary = PostSummary.From(postId, list,listens, count, hasLiked);

            return summary;
        }
        
    }
}