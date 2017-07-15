using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IAddableRepository<Post,Guid> , IUpdateableRepository<Post,Guid>, IDeleteableRepository<Post,Guid>
    {
        Task<IReadOnlyList<Post>> FindAllPostsPaginated(string continuationToken);
        Task<IReadOnlyList<Post>> FindAllPostsPaginated(Guid userId, string continuationToken);
    }
}
