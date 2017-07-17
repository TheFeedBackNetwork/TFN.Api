using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.LikeAggregate.Document
{
    [CollectionOptions("messageMetadata", "like")]
    public sealed class LikeDocumentModel : LikeDocumentModel<Guid>
    {
        public LikeDocumentModel(Guid id, DateTime created)
            : base(id,created)
        {
            
        }

        public LikeDocumentModel()
        {
            
        }
    }

    public class LikeDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        public LikeDocumentModel(TKey id, DateTime created)
            : base(id,"like",created,created)
        {
            
        }

        public LikeDocumentModel()
        {

        }
    }
}