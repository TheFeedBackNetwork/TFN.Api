using System;
using System.Collections.Generic;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Models.Entities
{
    public class Track : DomainEntity<Guid>, IAggregateRoot
    {
        public Uri Location { get; private set; }
        public Guid UserId { get; private set; }
        public TrackMetaData TrackMetaData { get; private set; }
        public IReadOnlyList<int> SoundWave { get; private set; }
        public DateTime Created { get; private set; }

        public Track(Guid id, Guid userId, Uri location, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
            : base(id)
        {
            if (soundWave.Count < 4000)
            {
                throw new ArgumentException($"The soundwave [{nameof(soundWave)}] must have 4000 or more data points.");
            }

            if (metaData == null)
            {
                throw new ArgumentNullException($"The track metadata [{metaData}] cannot be null.");
            }

            UserId = userId;
            Location = location;
            SoundWave = soundWave;
            TrackMetaData = metaData;
            Created = created;
        }

        public Track(Guid userId, Uri location, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
            : this(Guid.NewGuid(), userId, location, soundWave,metaData, created)
        {
            
        }

        public static Track Hydrate(Guid id, Guid userId, Uri location, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
        {
            return new Track(id,userId,location,soundWave,metaData,created);
        }

    }
}
