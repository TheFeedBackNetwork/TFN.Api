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

namespace TFN.Infrastructure.Repositories.CommentAggregate.Document
{
    public class CommentDocumentRepository : CachedDocumentRepository<Comment, CommentDocumentModel, Guid>, ICommentRepository
    {
        public CommentDocumentRepository(
            IAggregateMapper<Comment, CommentDocumentModel, Guid> mapper, DocumentContext context,
            ILogger<CommentDocumentRepository> logger,
            IAggregateCache<Comment> cache)
            : base(mapper,cache, context, logger)
        {
            
        }

        public async Task<IReadOnlyList<Comment>> FindCommentsPaginated(Guid postId, string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.PostId == postId && x.Type == Type, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates?.ToList();
        }

        public async Task<int> Count(Guid postId)
        {
            var count = await Collection.Count(x => x.PostId == postId && x.Type == Type);

            return count;
        }
        
    }
}