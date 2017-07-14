using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.LikeAggregate.Document
{
    public class LikeDocumentRepository : DocumentRepository<Like,LikeDocumentModel,Guid>, ILikeRepository
    {
        public LikeDocumentRepository(
            IAggregateMapper<Like, LikeDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<LikeDocumentRepository> logger)
            :base(mapper,context,logger)
        {
            
        }

        public async Task<int> Count(Guid postId)
        {
            var count = await Collection.Count(x => x.PostId == postId && x.Type == Type);

            return count;
        }

        public async Task<bool> Exists(Guid postId, Guid userId)
        {
            return await Collection.Any(x => x.UserId == userId && x.PostId == postId && x.Type == Type);
        }

        public async Task<IReadOnlyList<Like>> FindLikesPaginated(Guid postId, string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.PostId == postId && x.Type == Type, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates?.ToList();
        }
    }
}