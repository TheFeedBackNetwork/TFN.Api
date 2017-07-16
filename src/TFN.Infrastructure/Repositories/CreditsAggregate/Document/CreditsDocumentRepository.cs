using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Cache;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Repositories.CreditsAggregate.Document
{
    public class CreditsDocumentRepository : CachedDocumentRepository<Credits, CreditsDocumentModel, Guid>, ICreditRepository
    {
        public CreditsDocumentRepository(
            IAggregateMapper<Credits, CreditsDocumentModel, Guid> mapper, DocumentContext context,
            ILogger<CreditsDocumentRepository> logger,
            IAggregateCache<Credits> cache)
            : base(mapper, cache, context, logger)
        {
            
        }

        public async Task<Credits> FindByUserId(Guid userId)
        {
            var document = await Collection.Find(x => x.UserId == userId && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<Credits> FindByUsername(string username)
        {
            var document = await Collection.Find(x => x.NormalizedUsername == username.ToUpperInvariant() && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<IReadOnlyList<Credits>> FindHighestCredits(string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(null,x=> x.TotalCredits, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates.ToList();
        }

        public async Task<IReadOnlyList<Credits>> FindUsers(string searchToken, string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.Type == Type && x.NormalizedUsername.Contains(searchToken.ToUpperInvariant()), continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates.ToList();
        }
    }
}