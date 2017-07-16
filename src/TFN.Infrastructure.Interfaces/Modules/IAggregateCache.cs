using System;

namespace TFN.Infrastructure.Interfaces.Modules
{
    public interface IAggregateCache<TAggregate>
    {
        TAggregate Find(Guid id);      
        void Add(TAggregate aggregate);
    }
}