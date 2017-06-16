using System;
using IdentityServer4.Models;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities.IdentityServer
{
    public class ApplicationClient : DomainEntity<Guid>, IAggregateRoot
    {
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public Client Client { get; private set; }

        private ApplicationClient(Guid id, DateTime created, DateTime modified, Client client)
            : base(id)
        {
            if (created > modified)
            {
                throw new ArgumentException($"[{nameof(created)}] cannot be set that far in the past.");
            }

            if (client == null)
            {
                throw new ArgumentNullException($"[{nameof(client)}] cannot be null.");
            }

            Created = created;
            Modified = modified;
            Client = client;
        }

        public ApplicationClient(Client client)
            : this(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, client)
        {

        }

        public static ApplicationClient Hydrate(Guid id, DateTime created, DateTime modified, Client client)
        {
            return new ApplicationClient(id,created, modified, client);
        }


    }
}