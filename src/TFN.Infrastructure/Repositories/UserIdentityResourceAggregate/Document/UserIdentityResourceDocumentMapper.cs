using System;
using System.Linq;
using IdentityServer4.Models;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Infrastructure.Architecture.Documents.Models;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.UserIdentityResourceAggregate.Document
{
    public class UserIdentityResourceDocumentMapper : IAggregateMapper<UserIdentityResource, UserIdentityResourceDocumentModel, Guid>
    {
        public UserIdentityResource CreateFrom(UserIdentityResourceDocumentModel dataEntity)
        {
            return UserIdentityResource.Hydrate(dataEntity.Id, dataEntity.Created, dataEntity.Modified, CreatePartialFrom(dataEntity));
        }

        public UserIdentityResourceDocumentModel CreateFrom(UserIdentityResource domainEntity)
        {
            var identityResource = domainEntity.IdentityResource;

            return new UserIdentityResourceDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Modified)
            {
                Enabled = identityResource.Enabled,
                Name = identityResource.Name,
                DisplayName = identityResource.DisplayName,
                Description = identityResource.Description,
                Required = identityResource.Required,
                Emphasize = identityResource.Emphasize,
                ShowInDiscoveryDocument = identityResource.ShowInDiscoveryDocument,
                UserClaims = identityResource.UserClaims.Select(x => new UserClaimDocumentModel { Type = x }).ToList()
            };
        }

        private IdentityResource CreatePartialFrom(UserIdentityResourceDocumentModel dataEntity)
        {
            return new IdentityResource
            {
                Enabled = dataEntity.Enabled,
                Name = dataEntity.Name,
                DisplayName = dataEntity.DisplayName,
                Description = dataEntity.Description,
                Required = dataEntity.Required,
                Emphasize = dataEntity.Emphasize,
                ShowInDiscoveryDocument = dataEntity.ShowInDiscoveryDocument,
                UserClaims = dataEntity.UserClaims.Select(x => x.Type).ToList()
            };
        }
    }
}