using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.UserIdentityResourceAggregate.Document
{
    public class UserIdentityResourceDocumentRepository : DocumentRepository<UserIdentityResource, UserIdentityResourceDocumentModel, Guid>, IUserIdentityResourceRepository
    {
        public UserIdentityResourceDocumentRepository(
            IAggregateMapper<UserIdentityResource, UserIdentityResourceDocumentModel, Guid> mapper,
            DocumentContext context, ILogger<UserIdentityResourceDocumentRepository> logger)
            : base(mapper, context, logger)
        {

        }

        public async Task<IReadOnlyCollection<UserIdentityResource>> FindAll()
        {
            var documents = await Collection.FindAll();

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyCollection<UserIdentityResource>> FindAll(IReadOnlyCollection<string> scopeNames)
        {
            var documents = await Collection.FindAll(x => scopeNames.Contains(x.Name));

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates.ToList().AsReadOnly();
        }
    }
}