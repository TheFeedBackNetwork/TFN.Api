using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.PostAggregate.Document
{
    public class PostDocumentRepository : DocumentRepository<Post, PostDocumentModel, Guid>, IPostRepository
    {
        public PostDocumentRepository(
            IAggregateMapper<Post, PostDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<PostDocumentRepository> logger)
            : base(mapper, context, logger)
        {
            
        }

        public async Task<IReadOnlyList<Post>> FindAllPostsPaginated(string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(null, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates?.ToList();
        }

        public async Task<IReadOnlyList<Post>> FindAllPostsPaginated(Guid userId,string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.UserId == userId, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates?.ToList();
        }

        public Task DeleteLike(Guid postId, Guid likeId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Like>> FindAllLikes(Guid postId, string continuationToken)
        {
            throw new NotImplementedException();
        }



        public Task<PostSummary> FindPostLikeSummary(Guid postId, int limit, string username)
        {
            throw new NotImplementedException();
        }

        public Task<Like> GetLikeAsync(Guid postId, Guid likeId)
        {
            throw new NotImplementedException();
        }
    }
}