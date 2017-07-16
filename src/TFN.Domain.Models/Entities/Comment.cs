using System;
using Newtonsoft.Json;
using TFN.Domain.Architecture.Attributes;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities
{
    [CacheVersion(0)]
    public class Comment : MessageDomainEntity, IAggregateRoot
    {
        public Guid PostId { get; private set; }

        [JsonConstructor]
        private  Comment(Guid id, Guid userId, Guid postId, string username, string text, bool isActive, DateTime created, DateTime modified)
            : base(id,userId,username,text,isActive,created,modified)
        {
            PostId = postId;
        }

        public Comment(Guid userId,Guid postId,string username, string text)
            :this(Guid.NewGuid(), userId,postId,username, text, true, DateTime.UtcNow, DateTime.UtcNow)
        {
            
        }

        public static Comment Hydrate(Guid id, Guid userId, Guid postId, string username, string text, bool isActive, DateTime created, DateTime modified)
        {
            return new Comment(id,userId,postId,username,text,isActive,created,modified);
        }

    }
}
