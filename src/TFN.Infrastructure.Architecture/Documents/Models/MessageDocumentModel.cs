using System;
using Newtonsoft.Json;

namespace TFN.Infrastructure.Architecture.Documents.Models
{
    public class MessageDocumentModel : MessageDocumentModel<Guid>
    {
        public MessageDocumentModel()
        {
            
        }

        public MessageDocumentModel(Guid id, Guid userId, string text, bool isActive, string type, DateTime created, DateTime modified)
            : base(id,userId,text,isActive,type,created,modified)
        {
            
        }
    }
    public class MessageDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        public MessageDocumentModel()
        {

        }

        public MessageDocumentModel(TKey id, Guid userId, string text, bool isActive, string type, DateTime created, DateTime modified)
            : base(id, type,created,modified)
        {
            UserId = userId;
            Text = text;
            IsActive = isActive;
        }
    }
}