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
        where TDocument : BaseDocument<TKey>
    {
        protected IAggregateMapper<TAggregate, TDocument, TKey> Mapper { get; }
        protected DocumentContext Context { get; }
        protected ILogger Logger { get; }

        protected DocumentRepository(IAggregateMapper<TAggregate,TDocument,TKey> mapper,DocumentContext context, ILogger logger)
        {
            Context = context;
            Logger = logger;
            Mapper = mapper;
        }

        public virtual async Task<TAggregate> Find(TKey id)
        {
            var document = await Context
                .Collection<TDocument>()
                .Find(id.ToString());

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

            await Context
                .Collection<TDocument>()
                .Add(document);
        }

        public virtual async Task Delete(TKey id)
        {
            await Context
                .Collection<TDocument>()
                .Delete(id.ToString());
        }

        public virtual async Task Update(TAggregate aggregate)
        {
            var document = Mapper.CreateFrom(aggregate);

            document.Modified = DateTime.UtcNow;

            await Context
                .Collection<TDocument>()
                .Update(document, document.Id.ToString());
        }
    }
}