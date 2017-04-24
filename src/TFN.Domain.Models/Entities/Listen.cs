using System;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.Enums;

namespace TFN.Domain.Models.Entities
{
    public class Listen : DomainEntity<Guid>
    {
        public Guid PostId { get; private set; }
        public Listener Listener { get; private set; }
        public string Username { get; private set; }
        public string IPAddress { get; private set; }   
        public DateTime Created { get; private set; }

        private Listen(Guid id, Guid postId, Listener listener, string username, string ipAddress, DateTime created)
            : base(id)
        {
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
