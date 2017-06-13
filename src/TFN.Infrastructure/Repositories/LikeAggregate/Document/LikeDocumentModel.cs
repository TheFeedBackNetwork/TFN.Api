using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;

namespace TFN.Infrastructure.Repositories.LikeAggregate.Document
{
    public sealed class LikeDocumentModel : LikeDocumentModel<Guid>
    {
        public LikeDocumentModel(Guid id, DateTime created)
            : base(id,created)
        {
            
        }
    }

    public class LikeDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        public LikeDocumentModel(TKey id, DateTime created)
            : base(id,"like",created,created)
        {
            
        }
    }
}