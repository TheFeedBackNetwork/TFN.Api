using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TFN.Domain.Architecture.Attributes;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Models.Entities
{
    [CacheVersion(0)]
    public class Track : DomainEntity<Guid>, IAggregateRoot
    {
        public Uri Location { get; private set; }
        public string TrackName { get; private set; }
        public Guid UserId { get; private set; }
        public TrackMetaData TrackMetaData { get; private set; }
        public IReadOnlyList<int> SoundWave { get; private set; }
        public DateTime Created { get; private set; }

        [JsonConstructor]
        public Track(Guid id, Guid userId, Uri location, string trackName, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
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
            TrackName = trackName;
            SoundWave = soundWave;
            TrackMetaData = metaData;
            Created = created;
        }

        public Track(Guid userId, Uri location, string trackName, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
            : this(Guid.NewGuid(), userId, location,trackName, soundWave,metaData, created)
        {
            
        }

        public static Track Hydrate(Guid id, Guid userId, Uri location,string trackName, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
        {
            return new Track(id,userId,location,trackName,soundWave,metaData,created);
        }

        public void ChangeTrackName(string trackName)
        {
            TrackName = trackName;
        }

    }
}
