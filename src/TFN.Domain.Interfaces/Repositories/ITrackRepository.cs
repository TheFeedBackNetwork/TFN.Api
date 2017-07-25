using System;
using TFN.Domain.Architecture.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ITrackRepository : IAddableRepository<Track, Guid>, IDeleteableRepository<Track,Guid>, IUpdateableRepository<Track,Guid>
    {

    }
}
