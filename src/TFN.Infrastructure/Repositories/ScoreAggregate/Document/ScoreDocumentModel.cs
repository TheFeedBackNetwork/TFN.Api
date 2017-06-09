using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;

namespace TFN.Infrastructure.Repositories.ScoreAggregate.Document
{
    public class ScoreDocumentModel : ScoreDocumentModel<Guid>
    {
        public ScoreDocumentModel(Guid id, DateTime created)
            :base(id,created)
        {
            
        }
    }

    public class ScoreDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "commentId")]
        public Guid CommentId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        public ScoreDocumentModel(TKey id, DateTime created)
            : base(id, "score", created,created)
        {
            
        }
    }
}