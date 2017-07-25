using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.TrackAggregate.Document
{
    [CollectionOptions("tracks", "track")]
    public sealed class TrackDocumentModel : TrackDocumentModel<Guid>
    {
        public TrackDocumentModel()
        {
            
        }
        public TrackDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id, created, modified)
        {
            
        }
    }
    public class TrackDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "trackName")]
        public string TrackName { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "trackMetadata")]
        public TrackMetaDataDocumentModel TrackMetaData { get; set; }

        [JsonProperty(PropertyName = "soundWave")]
        public ICollection<int> SoundWave { get; set; }

        public TrackDocumentModel()
        {
            
        }

        public TrackDocumentModel(TKey id, DateTime created, DateTime modified)
            : base(id, "track", created, modified)
        {
            
        }
    }

    public class TrackMetaDataDocumentModel
    {
        [JsonProperty(PropertyName = "hours")]
        public int Hours { get; set; }

        [JsonProperty(PropertyName = "minutes")]
        public int Minutes { get; set; }

        [JsonProperty(PropertyName = "seconds")]
        public int Seconds { get; set; }

        [JsonProperty(PropertyName = "totalHours")]
        public double TotalHours { get; set; }

        [JsonProperty(PropertyName = "totalMinutes")]
        public double TotalMinutes { get; set; }

        [JsonProperty(PropertyName = "totalMilliseconds")]
        public double TotalMilliseconds { get; set; }

        [JsonProperty(PropertyName = "ticks")]
        public long Ticks { get; set; }
    }
}