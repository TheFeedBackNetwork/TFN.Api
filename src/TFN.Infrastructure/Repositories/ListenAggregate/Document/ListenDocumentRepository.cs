using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Cache;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Repositories.ListenAggregate.Document
{
    public class ListenDocumentRepository : CachedDocumentRepository<Listen,ListenDocumentModel,Guid>, IListenRepository
    {
        public ListenDocumentRepository(
            IAggregateMapper<Listen, ListenDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<ListenDocumentRepository> logger,
            IAggregateCache<Listen> cache)
            : base(mapper, cache, context, logger)
        {
            
        }

        public async Task<int> Count(Guid postId)
        {
            var count = await Collection.Count(x => x.PostId == postId && x.Type == Type);

            return count;
        }
    }
}