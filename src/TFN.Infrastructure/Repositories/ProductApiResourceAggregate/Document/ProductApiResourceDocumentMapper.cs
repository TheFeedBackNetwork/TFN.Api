using System;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.ProductApiResourceAggregate.Document
{
    public class ProductApiResourceDocumentMapper : IAggregateMapper<ProductApiResource, ProductApiResourceDocumentModel, Guid>
    {
        public ProductApiResource CreateFrom(ProductApiResourceDocumentModel dataEntity)
        {
            throw new NotImplementedException();
        }

        public ProductApiResourceDocumentModel CreateFrom(ProductApiResource domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}