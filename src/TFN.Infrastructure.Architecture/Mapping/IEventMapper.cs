using TFN.Domain.Architecture.Models;
using TFN.Infrastructure.Architecture.Records;

namespace TFN.Infrastructure.Architecture.Mapping
{
    public interface IEventMapper<in TEvent, out TRecord, TKey>
        where TEvent : DomainEvent<TKey>
        where TRecord : BaseRecord
    {
        TRecord CreateFrom(TEvent domainEvent);
    }
}