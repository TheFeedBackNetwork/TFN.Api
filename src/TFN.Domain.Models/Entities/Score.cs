using System;
using Newtonsoft.Json;
using TFN.Domain.Architecture.Attributes;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities
{
    [CacheVersion(0)]
    public class Score : DomainEntity<Guid>, IAggregateRoot
    {
        public Guid CommentId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Created { get; private set; }

        [JsonConstructor]
        private Score(Guid id, Guid commentId, Guid userId, DateTime created)
            : base(id)
        {

            CommentId = commentId;
            UserId = userId;
            Created = created;
        }

        public Score(Guid commentId, Guid userId)
            : this(Guid.NewGuid(),commentId,userId,DateTime.UtcNow)
        {
        }

        public static Score Hydrate(Guid id, Guid commentId, Guid userId, DateTime created)
        {
            return new Score(id,commentId,userId,created);
        }
    }
}
