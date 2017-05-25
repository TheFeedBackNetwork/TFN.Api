using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using TFN.Domain.Architecture.Models;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Records;

namespace TFN.Infrastructure.Architecture.Repositories.Record
{
    public class RecordRepository<TDomainEvent, TRecord, TKey>
        where TDomainEvent : DomainEvent<TKey>
        where TRecord : BaseRecord
    {
        protected IEventMapper<TDomainEvent, TRecord, TKey> Mapper { get; }
        protected RecordContext Context { get; }


        protected RecordRepository(IEventMapper<TDomainEvent, TRecord, TKey> mapper, RecordContext context)
        {
            Mapper = mapper;
            Context = context;
        }

        public virtual async Task Save(TDomainEvent domainEvent)
        {
            var record = Mapper.CreateFrom(domainEvent);

            var table = await Context.RecordTable<TRecord>(); 

            var request = TableOperation.Insert(record);

            await table.ExecuteAsync(request);

        }

        /*public virtual async Task Source(string id)
        {
            var table = await Context.RecordTable<TRecord>();

            var query = new TableQuery<TRecord>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id))
                );

            var record = table.ExecuteQuery(query);
        }*/
    }
}