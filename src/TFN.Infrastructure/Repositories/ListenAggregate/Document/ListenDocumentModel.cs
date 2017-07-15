using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.ListenAggregate.Document
{
    [CollectionOptions("messageMetadata", "listen")]
    public sealed class ListenDocumentModel : ListenDocumentModel<Guid>
    {
        public ListenDocumentModel(Guid id, DateTime created)
            : base(id,created)
        {
            
        }

        public ListenDocumentModel()
        {
            
        }
    }

    public class ListenDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "listener")]
        public string Listener { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "iPaddress")]
        public string IPAddress { get; set; }

        public ListenDocumentModel(TKey id, DateTime created)
            : base(id,"listen",created,created)
        {
            
        }

        public ListenDocumentModel()
        {

        }
    }
}