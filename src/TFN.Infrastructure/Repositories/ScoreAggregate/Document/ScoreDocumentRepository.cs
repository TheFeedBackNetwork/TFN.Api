using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.ScoreAggregate.Document
{
    public class ScoreDocumentRepository : DocumentRepository<Score,ScoreDocumentModel,Guid>, IScoreRepository
    {
        public ScoreDocumentRepository(
            IAggregateMapper<Score, ScoreDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<ScoreDocumentRepository> logger)
            : base(mapper, context, logger)
        {
            
        }

        public async Task<int> Count(Guid commentId)
        {
            var count = await Collection.Count(x => x.CommentId == commentId);

            return count;
        }

        public async Task<IReadOnlyList<Score>> FindAllPaginated(Guid comentId, string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.CommentId == comentId, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);
            
            return aggregates?.ToList();
        }
    }
}