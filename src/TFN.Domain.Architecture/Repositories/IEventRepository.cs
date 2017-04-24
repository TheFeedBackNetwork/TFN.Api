using System.Threading.Tasks;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Architecture.Repositories
{
    public interface IEventRepository<in TDomainEvent, TKey>
        where TDomainEvent : DomainEvent<TKey>
    {
        Task Save(TDomainEvent domainEvent);
    }
}