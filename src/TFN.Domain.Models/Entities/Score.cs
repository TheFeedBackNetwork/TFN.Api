using System;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities
{
    public class Score : DomainEntity<Guid>
    {
        public Guid CommentId { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public DateTime Created { get; private set; }

        private Score(Guid id,Guid commentId, Guid userId,string username, DateTime created)
            : base(id)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException($"The username [{nameof(username)}] is either null or empty.");
            }
            if (username.Length < 3)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too short.");
            }
            if (username.Length > 16)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too long.");
            }

            CommentId = commentId;
            UserId = userId;
            Username = username;
            Created = created;
        }

        public Score(Guid commentId,Guid userId, string username)
            : this(Guid.NewGuid(),commentId,userId,username,DateTime.UtcNow)
        {
        }

        public static Score Hydrate(Guid id,Guid commentId, Guid userId,string username, DateTime created)
        {
            return new Score(id,commentId,userId,username,created);
        }
    }
}
