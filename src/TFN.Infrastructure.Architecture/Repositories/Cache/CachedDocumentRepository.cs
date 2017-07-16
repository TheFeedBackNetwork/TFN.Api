using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Architecture.Models;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Architecture.Repositories.Cache
{
    public abstract class CachedDocumentRepository<TAggregate, TDocument, TKey>
        : DocumentRepository<TAggregate,TDocument,TKey>
        where TAggregate : DomainEntity<TKey>, IAggregateRoot
        where TDocument : BaseDocument<TKey>, new()
    {
        protected IAggregateCache<TAggregate> Cache { get; }

        protected CachedDocumentRepository(
            IAggregateMapper<TAggregate, TDocument, TKey> mapper, IAggregateCache<TAggregate> cache,
            DocumentContext context, ILogger logger)
            : base(mapper,context,logger)
        {
            Cache = cache;
        }

        public override async Task<TAggregate> Find(TKey id)
        {
            var cachedAggregate = Cache.Find(id.ToString());

            if (cachedAggregate != null)
            {
                return cachedAggregate;
            }

            var document = await Collection.Find(id.ToString());

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public override async Task Add(TAggregate aggregate)
        {
            var document = Mapper.CreateFrom(aggregate);

            await Collection.Add(document);

            Cache.Add(aggregate);
        }

        public override async Task Delete(TKey id)
        {
            await Collection.Delete(id.ToString());

            Cache.Delete(id.ToString());
        }

        public override async Task Update(TAggregate aggregate)
        {
            var document = Mapper.CreateFrom(aggregate);

            document.Modified = DateTime.UtcNow;

            await Collection.Update(document, document.Id.ToString());

            Cache.Add(aggregate);
        }
    }
}