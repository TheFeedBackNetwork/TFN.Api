using System;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Cache;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Repositories.TrackAggregate.Document
{
    public class TrackDocumentRepository : CachedDocumentRepository<Track, TrackDocumentModel, Guid>, ITrackRepository
    {
        public TrackDocumentRepository(
            IAggregateMapper<Track, TrackDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<TrackDocumentRepository> logger,
            IAggregateCache<Track> cache)
            : base(mapper, cache, context, logger)
        {
            
        }


    }
}