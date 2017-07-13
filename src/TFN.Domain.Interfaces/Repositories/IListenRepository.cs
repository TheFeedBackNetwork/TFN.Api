using System;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IListenRepository : IAddableRepository<Listen, Guid>
    {
        Task<int> Count(Guid postId);
    }
}