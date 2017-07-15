using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Architecture.Models;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public class DocumentRepository<TAggregate, TDocument, TKey>
        where TAggregate : DomainEntity<TKey>, IAggregateRoot
        where TDocument : BaseDocument<TKey>, new()
    {
        protected IAggregateMapper<TAggregate, TDocument, TKey> Mapper { get; }
        protected DocumentContext Context { get; }
        protected IDocumentCollection<TDocument> Collection { get; }
        protected ILogger Logger { get; }
        protected string Type { get; }

        protected DocumentRepository(IAggregateMapper<TAggregate,TDocument,TKey> mapper,DocumentContext context, ILogger logger)
        {
            Context = context;
            Collection = context.Collection<TDocument>();
            Logger = logger;
            Mapper = mapper;
            Type = context.GetType<TDocument>();
        }

        public virtual async Task<TAggregate> Find(TKey id)
        {
            var document = await Collection.Find(id.ToString());
            
            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public virtual async Task Add(TAggregate aggregate)
        {
            var document = Mapper.CreateFrom(aggregate);

            await Collection.Add(document);
        }

        public virtual async Task Delete(TKey id)
        {
            await Collection.Delete(id.ToString());
        }

        public virtual async Task Update(TAggregate aggregate)
        {
            var document = Mapper.CreateFrom(aggregate);

            document.Modified = DateTime.UtcNow;

            await Collection.Update(document, document.Id.ToString());
        }

        public virtual async Task<bool> Any()
        {
            
            return await Collection.Any();
        }
    }
}