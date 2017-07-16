using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Cache;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Repositories.ApplicationClientAggregate.Document
{
    public class ApplicationClientDocumentRepository : CachedDocumentRepository<ApplicationClient, ApplicationClientDocumentModel, Guid>, IApplicationClientRepository
    {
        public ApplicationClientDocumentRepository(
            IAggregateMapper<ApplicationClient, ApplicationClientDocumentModel, Guid> mapper, 
            DocumentContext context,
            ILogger<ApplicationClientDocumentRepository> logger,
            IAggregateCache<ApplicationClient> cache)
            : base(mapper, cache, context, logger)
        {
        }
        public async Task<ApplicationClient> Find(string clientId)
        {
            var document = await Collection.Find(x => x.ClientId == clientId && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<IReadOnlyCollection<string>> FindAllAllowedCorsOrigins()
        {
            //might want to limit the findall to only internal clients
            var documents = await Collection.FindAll(x => x.Type == Type);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            var allAllowedOrigins = aggregates.SelectMany(x => x.Client.AllowedCorsOrigins);

            return allAllowedOrigins?.ToList().AsReadOnly();
        }
        
    }
}