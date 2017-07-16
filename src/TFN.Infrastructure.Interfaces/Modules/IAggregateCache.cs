using System;

namespace TFN.Infrastructure.Interfaces.Modules
{
    public interface IAggregateCache<TAggregate>
    {
        TAggregate Find(string id);      
        void Add(TAggregate aggregate);
        void Delete(string id);
    }
}