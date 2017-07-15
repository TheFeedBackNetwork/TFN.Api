using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents.Models;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.CommentAggregate.Document
{
    [CollectionOptions("messages", "comment")]
    public sealed class CommentDocumentModel : CommentDocumentModel<Guid>
    {
        public CommentDocumentModel()
        {

        }

        public CommentDocumentModel(Guid id, Guid userId, string username, string text, bool isActive, DateTime created,
            DateTime modified)
            : base(id,userId, username, text, isActive, created, modified)
        {

        }
    }

    public class CommentDocumentModel<TKey> : MessageDocumentModel<TKey> 
    {
        [JsonProperty(PropertyName = "commentId")]
        public Guid PostId { get; set; }

        public CommentDocumentModel()
        {
            
        }

        public CommentDocumentModel(TKey id, Guid userId, string username, string text, bool isActive, DateTime created,
            DateTime modified)
            : base(id,userId,username,text,isActive,"comment",created,modified)
        {

        }
    }
}