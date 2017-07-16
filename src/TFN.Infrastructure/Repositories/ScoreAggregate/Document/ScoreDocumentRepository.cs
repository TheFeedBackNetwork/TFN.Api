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

namespace TFN.Infrastructure.Repositories.ScoreAggregate.Document
{
    public class ScoreDocumentRepository : CachedDocumentRepository<Score,ScoreDocumentModel,Guid>, IScoreRepository
    {
        public ScoreDocumentRepository(
            IAggregateMapper<Score, ScoreDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<ScoreDocumentRepository> logger,
            IAggregateCache<Score> cache)
            : base(mapper, cache, context, logger)
        {
            
        }

        public async Task<int> Count(Guid commentId)
        {
            var count = await Collection.Count(x => x.CommentId == commentId && x.Type == Type);

            return count;
        }

        public async Task<bool> Exists(Guid commentId, Guid userId)
        {
            return await Collection.Any(x => x.UserId == userId && x.CommentId == commentId && x.Type == Type);
        }

        public async Task<Score> Find(Guid commentId, Guid userId)
        {
            var document = await Collection.Find(x => x.CommentId == commentId && x.UserId == userId && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<IReadOnlyList<Score>> FindScoresPaginated(Guid comentId, string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.CommentId == comentId && x.Type == Type, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);
            
            return aggregates?.ToList();
        }
    }
}