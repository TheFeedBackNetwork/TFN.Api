using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.ListenAggregate.Document
{
    public class ListenDocumentRepository : DocumentRepository<Listen,ListenDocumentModel,Guid>, IListenRepository
    {
        public ListenDocumentRepository(
            IAggregateMapper<Listen, ListenDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<ListenDocumentRepository> logger)
            : base(mapper, context, logger)
        {
            
        }

        public async Task<int> Count(Guid postId)
        {
            var count = await Collection.Count(x => x.PostId == postId && x.Type == Type);

            return count;
        }
    }
}