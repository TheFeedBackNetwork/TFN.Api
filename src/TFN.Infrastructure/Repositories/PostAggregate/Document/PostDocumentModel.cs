using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents.Models;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.PostAggregate.Document
{
    [CollectionOptions("messages", "post")]
    public sealed class PostDocumentModel : PostDocumentModel<Guid>
    {
        public PostDocumentModel(Guid id, Guid userId,string text, bool isActive, DateTime created,
            DateTime modified)
            : base(id,userId,text, isActive, created, modified)
        {
            
        }

        public PostDocumentModel()
        {
            
        }
    }

    public class PostDocumentModel<TKey> : MessageDocumentModel<TKey>
    {
        [JsonProperty(PropertyName = "trackUrl")]
        public string TrackUrl { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public ICollection<string> Tags { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }


        public PostDocumentModel(TKey id, Guid userId, string text, bool isActive, DateTime created,
            DateTime modified)
            : base(id,userId,text,isActive,"post",created,modified)
        {
            
        }

        public PostDocumentModel()
        {

        }
    }
}