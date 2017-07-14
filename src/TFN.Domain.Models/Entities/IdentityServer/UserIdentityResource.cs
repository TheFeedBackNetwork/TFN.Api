using System;
using IdentityServer4.Models;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities.IdentityServer
{
    public class UserIdentityResource : DomainEntity<Guid>, IAggregateRoot
    {
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public IdentityResource IdentityResource { get; private set; }

        private UserIdentityResource(Guid id, DateTime created, DateTime modified, IdentityResource identityResource)
            : base(id)
        {
            if (created > modified)
            {
                throw new ArgumentException($"{nameof(created)} cannot be set that far in the past.");
            }

            if (identityResource == null)
            {
                throw new ArgumentNullException($"{nameof(identityResource)} cannot be null.");
            }

            IdentityResource = identityResource;
            Created = created;
            Modified = modified;
        }

        public UserIdentityResource(IdentityResource identityResource)
            : this(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, identityResource)
        {

        }

        public static UserIdentityResource Hydrate(Guid id, DateTime created, DateTime modified,
            IdentityResource identityResource)
        {
            return new UserIdentityResource(id, created, modified, identityResource);
        }
    }
}