using System;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities
{
    public class Like : DomainEntity<Guid>, IAggregateRoot
    {
        public Guid PostId { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public DateTime Created { get; private set; }

        private Like(Guid id, Guid postId, Guid userId, string username, DateTime created)
            : base(id)
        {

            PostId = postId;
            UserId = userId;
            Username = username;
            Created = created;
        }

        public Like(Guid postId, Guid userId, string username)
            : this(Guid.NewGuid(),postId,userId,username, DateTime.UtcNow)
        {
            
        }

        public static Like Hydrate(Guid id, Guid postId, Guid userId, string username, DateTime created)
        {
            return new Like(id,postId,userId,username,created);
        }
    }
}