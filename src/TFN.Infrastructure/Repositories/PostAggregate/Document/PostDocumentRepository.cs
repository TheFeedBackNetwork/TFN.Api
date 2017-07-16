using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Cache;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Repositories.PostAggregate.Document
{
    public class PostDocumentRepository : CachedDocumentRepository<Post, PostDocumentModel, Guid>, IPostRepository
    {
        public PostDocumentRepository(
            IAggregateMapper<Post, PostDocumentModel, Guid> mapper,
            IAggregateCache<Post> cache,
            DocumentContext context,
            ILogger<PostDocumentRepository> logger)
            : base(mapper,cache, context, logger)
        {
            
        }

        public async Task<IReadOnlyList<Post>> FindAllPostsPaginated(string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.Type == Type, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates?.ToList();
        }

        public async Task<IReadOnlyList<Post>> FindAllPostsPaginated(Guid userId,string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.UserId == userId && x.Type == Type, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates?.ToList();
        }
    }
}