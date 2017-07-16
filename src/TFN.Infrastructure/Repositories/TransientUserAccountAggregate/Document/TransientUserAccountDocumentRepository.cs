using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Cache;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Repositories.TransientUserAccountAggregate.Document
{
    public class TransientUserAccountDocumentRepository : CachedDocumentRepository<TransientUserAccount, TransientUserAccountDocumentModel, Guid> , ITransientUserAccountRepository
    {
        public TransientUserAccountDocumentRepository(
            IAggregateMapper<TransientUserAccount, TransientUserAccountDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<TransientUserAccountDocumentRepository> logger,
            IAggregateCache<TransientUserAccount> cache)
            : base(mapper, cache, context, logger)
        {
            
        }

        public async Task<TransientUserAccount> FindByEmail(string email)
        {
            var document = await Collection.Find(x => x.NormalizedEmail == email.ToUpperInvariant() && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<TransientUserAccount> FindByUsername(string username)
        {
            var document = await Collection.Find(x => x.NormalizedUsername == username.ToUpperInvariant() && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<TransientUserAccount> FindByVerificationKey(string verificationKey)
        {
            var document = await Collection.Find(x => x.VerificationKey == verificationKey && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }
    }
}