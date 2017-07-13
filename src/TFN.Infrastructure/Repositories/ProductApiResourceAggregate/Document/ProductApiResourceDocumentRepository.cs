using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.ProductApiResourceAggregate.Document
{
    public class ProductApiResourceDocumentRepository : DocumentRepository<ProductApiResource, ProductApiResourceDocumentModel, Guid>, IProductApiResourceRepository
    {
        public ProductApiResourceDocumentRepository(
            IAggregateMapper<ProductApiResource, ProductApiResourceDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<ProductApiResourceDocumentRepository> logger)
            : base(mapper, context, logger)
        {

        }

        public async Task<ProductApiResource> Find(string scopeName)
        {
            var document = await Collection.Find(x => x.Name == scopeName);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<IReadOnlyCollection<ProductApiResource>> FindAll(IReadOnlyCollection<string> scopeNames)
        {

            var documents = await Collection.FindAll(x => x.Type == Type);

            if (documents == null)
            {
                return null;
            }

            var results = documents.Where(x => x.Scopes.Any(y => scopeNames.Contains(y.Name)));

            var aggregates = results.Select(Mapper.CreateFrom);

            return aggregates.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyCollection<ProductApiResource>> FindAll()
        {
            var documents = await Collection.FindAll(x => x.Type == Type);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates.ToList().AsReadOnly();
        }
    }
}