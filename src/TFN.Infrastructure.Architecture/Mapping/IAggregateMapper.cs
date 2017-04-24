using TFN.Domain.Architecture.Models;
using TFN.Infrastructure.Architecture.Documents;

namespace TFN.Infrastructure.Architecture.Mapping
{
    public interface IAggregateMapper<TAggregate, TDocument, TKey>
        where TAggregate : DomainEntity<TKey>
        where TDocument : IDocument<TKey>
    {
        TAggregate CreateFrom(TDocument dataEntity);
        TDocument CreateFrom(TAggregate domainEntity);
    }

}