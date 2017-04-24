using System.Threading.Tasks;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Architecture.Repositories
{
    public interface IUpsertableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
       where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        Task Upsert(TDomainEntity domainEntity);
    }
}