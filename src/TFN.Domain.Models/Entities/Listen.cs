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
        public Guid? UserId { get; private set; }
        public string IPAddress { get; private set; }   
        public DateTime Created { get; private set; }

        [JsonConstructor]
        private Listen(Guid id, Guid postId, Listener listener, string ipAddress, DateTime created)
            : base(id)
        {
            if (listener.Equals(Listener.User))
            {

            }
            else
            {

            }

            PostId = postId;
            Listener = listener;
            IPAddress = ipAddress;
            Created = created;
        }

        public Listen(Guid postId, Listener listener, string ipAddress)
            : this(Guid.NewGuid(), postId,listener,ipAddress,DateTime.UtcNow)
        {
            
        }

        public static Listen Hydrate(Guid id, Guid postId, Listener listener, string ipAddress, DateTime created)
        {
            return new Listen(id,postId,listener,ipAddress,created);
        }
    }
}
