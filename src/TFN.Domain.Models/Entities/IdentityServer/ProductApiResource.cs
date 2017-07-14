using System;
using IdentityServer4.Models;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities.IdentityServer
{
    public class ProductApiResource : DomainEntity<Guid>, IAggregateRoot
    {
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public ApiResource ApiResource { get; private set; }

        private ProductApiResource(Guid id, DateTime created, DateTime modified, ApiResource apiResource)
            : base(id)
        {
            if (created > modified)
            {
                throw new ArgumentException($" [{nameof(created)}] cannot be set that far in the past.");
            }

            if (apiResource == null)
            {
                throw new ArgumentNullException($"[{nameof(apiResource)}] cannot be null.");
            }

            ApiResource = apiResource;
            Created = created;
            Modified = modified;
        }

        public ProductApiResource(ApiResource apiResource)
            : this(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, apiResource)
        {

        }

        public static ProductApiResource Hydrate(Guid id, DateTime created, DateTime modified, ApiResource apiResource)
        {
            return new ProductApiResource(id, created, modified, apiResource);
        }
    }
}