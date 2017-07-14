using System.Threading.Tasks;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Architecture.Repositories
{
    public interface IUpdateableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        Task Update(TDomainEntity domainEntity);
    }
}