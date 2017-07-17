using System;
using Newtonsoft.Json;
using TFN.Domain.Architecture.Attributes;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities
{
    [CacheVersion(0)]
    public class Like : DomainEntity<Guid>, IAggregateRoot
    {
        public Guid PostId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Created { get; private set; }

        [JsonConstructor]
        private Like(Guid id, Guid postId, Guid userId, DateTime created)
            : base(id)
        {

            PostId = postId;
            UserId = userId;
            Created = created;
        }

        public Like(Guid postId, Guid userId)
            : this(Guid.NewGuid(),postId,userId, DateTime.UtcNow)
        {
            
        }

        public static Like Hydrate(Guid id, Guid postId, Guid userId, DateTime created)
        {
            return new Like(id,postId,userId,created);
        }
    }
}