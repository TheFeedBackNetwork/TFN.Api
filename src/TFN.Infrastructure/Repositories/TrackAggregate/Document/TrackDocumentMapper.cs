using System;
using System.Linq;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.TrackAggregate.Document
{
    public class TrackDocumentMapper : IAggregateMapper<Track, TrackDocumentModel, Guid>
    {
        public Track CreateFrom(TrackDocumentModel dataEntity)
        {
            return Track.Hydrate(
                dataEntity.Id,
                dataEntity.UserId,
                new Uri(dataEntity.Location),
                dataEntity.SoundWave.ToList().AsReadOnly(),
                CreatePartialFrom(dataEntity.TrackMetaData),
                dataEntity.Created
                );
        }

        public TrackDocumentModel CreateFrom(Track domainEntity)
        {
            return new TrackDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Created)
            {
                Location = domainEntity.Location.OriginalString,
                SoundWave = domainEntity.SoundWave.ToList(),
                TrackMetaData = new TrackMetaDataDocumentModel
                {
                    Hours = domainEntity.TrackMetaData.Hours,
                    Minutes = domainEntity.TrackMetaData.Minutes,
                    Seconds = domainEntity.TrackMetaData.Seconds,
                    Ticks = domainEntity.TrackMetaData.Ticks,
                    TotalHours = domainEntity.TrackMetaData.TotalHours,
                    TotalMilliseconds = domainEntity.TrackMetaData.TotalMilliseconds,
                    TotalMinutes = domainEntity.TrackMetaData.TotalMinutes
                },
            };
        }

        private TrackMetaData CreatePartialFrom(TrackMetaDataDocumentModel dataEntity)
        {
            return TrackMetaData.From(
                dataEntity.Hours,
                dataEntity.Minutes,
                dataEntity.Seconds,
                dataEntity.TotalHours,
                dataEntity.TotalMinutes,
                dataEntity.TotalMilliseconds,
                dataEntity.Ticks);
        }
    }
}