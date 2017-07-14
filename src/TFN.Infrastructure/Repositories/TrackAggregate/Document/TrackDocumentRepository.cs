using System;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.TrackAggregate.Document
{
    public class TrackDocumentRepository : DocumentRepository<Track, TrackDocumentModel, Guid>, ITrackRepository
    {
        public TrackDocumentRepository(
            IAggregateMapper<Track, TrackDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<TrackDocumentRepository> logger)
            : base(mapper, context, logger)
        {
            
        }


    }
}