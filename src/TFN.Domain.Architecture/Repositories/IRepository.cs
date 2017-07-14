using System.Threading.Tasks;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Architecture.Repositories
{
    public interface IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        Task<TDomainEntity> Find(TKey id);

        //REMOVE THIS IS USED FOR SEEDDATAONLY
        Task<bool> Any();
    }
}