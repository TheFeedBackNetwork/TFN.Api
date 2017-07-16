using System;
using Newtonsoft.Json;
using TFN.Domain.Architecture.Attributes;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.Enums;

namespace TFN.Domain.Models.Entities
{
    [CacheVersion(0)]
    public class Listen : DomainEntity<Guid>, IAggregateRoot
    {
        public Guid PostId { get; private set; }
        public Listener Listener { get; private set; }
        public string Username { get; private set; }
        public string IPAddress { get; private set; }   
        public DateTime Created { get; private set; }

        [JsonConstructor]
        private Listen(Guid id, Guid postId, Listener listener, string username, string ipAddress, DateTime created)
            : base(id)
        {
            if (listener.Equals(Listener.User))
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
            }
            else
            {
                username = null;
            }

            PostId = postId;
            Username = username;
            Listener = listener;
            IPAddress = ipAddress;
            Created = created;
        }

        public Listen(Guid postId,Listener listener, string username, string ipAddress)
            : this(Guid.NewGuid(), postId,listener,username,ipAddress,DateTime.UtcNow)
        {
            
        }

        public static Listen Hydrate(Guid id, Guid postId,Listener listener, string username, string ipAddress, DateTime created)
        {
            return new Listen(id,postId,listener,username,ipAddress,created);
        }
    }
}
