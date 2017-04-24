using System.Threading.Tasks;
using TFN.Domain.Architecture.Models;
using Microsoft.WindowsAzure.Storage.Table;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Records;

namespace TFN.Infrastructure.Repositories.Base.Repositories.Record
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
    }
}