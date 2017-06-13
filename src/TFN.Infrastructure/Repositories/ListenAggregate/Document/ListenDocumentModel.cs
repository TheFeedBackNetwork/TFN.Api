using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;

namespace TFN.Infrastructure.Repositories.ListenAggregate.Document
{

    public sealed class ListenDocumentModel : ListenDocumentModel<Guid>
    {
        public ListenDocumentModel(Guid id, DateTime created)
            : base(id,created)
        {
            
        }
    }

    public class ListenDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; private set; }

        [JsonProperty(PropertyName = "listener")]
        public string Listener { get; private set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; private set; }

        [JsonProperty(PropertyName = "iPaddress")]
        public string IPAddress { get; private set; }
        public ListenDocumentModel(TKey id, DateTime created)
            : base(id,"listen",created,created)
        {
            
        }
    }
}