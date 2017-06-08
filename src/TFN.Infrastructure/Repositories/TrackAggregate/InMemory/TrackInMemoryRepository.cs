using System;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.TrackAggregate.InMemory
{
    public class TrackInMemoryRepository : ITrackRepository
    {
        public Task Add(Track entity)
        {
            InMemoryTracks.Tracks.Add(entity);
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            InMemoryTracks.Tracks.RemoveAll(x => x.Id == id);

            return Task.CompletedTask;
        }

        public Task<Track> Find(Guid id)
        {
            return Task.FromResult(InMemoryTracks.Tracks.SingleOrDefault(x => x.Id == id));
        }
    }
}