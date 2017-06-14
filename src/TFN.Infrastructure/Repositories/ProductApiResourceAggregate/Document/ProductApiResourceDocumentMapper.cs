using System;
using System.Linq;
using IdentityServer4.Models;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Infrastructure.Architecture.Documents.Models;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.ProductApiResourceAggregate.Document
{
    public class ProductApiResourceDocumentMapper : IAggregateMapper<ProductApiResource, ProductApiResourceDocumentModel, Guid>
    {
        public ProductApiResource CreateFrom(ProductApiResourceDocumentModel dataEntity)
        {
            return ProductApiResource.Hydrate(
                dataEntity.Id,
                dataEntity.Created,
                dataEntity.Modified,
                CreatePartialFrom(dataEntity)
                );
        }

        public ProductApiResourceDocumentModel CreateFrom(ProductApiResource domainEntity)
        {
            var resource = domainEntity.ApiResource;

            return new ProductApiResourceDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Modified)
            {
                Enabled = resource.Enabled,
                Name = resource.Name,
                DisplayName = resource.DisplayName,
                Description = resource.Description,
                Secrets = resource.ApiSecrets.Select(x => new SecretDocumentModel
                {
                    Description = x.Description,
                    Expiration = x.Expiration,
                    Type = x.Type,
                    Value = x.Value
                }).ToList(),
                Scopes = resource.Scopes.Select(x => new ApiScopeDocumentModel
                {
                    Description = x.Description,
                    DisplayName = x.DisplayName,
                    Emphasize = x.Emphasize,
                    Name = x.Name,
                    Required = x.Required,
                    ShowInDiscoveryDocument = x.ShowInDiscoveryDocument,
                    UserClaims = x.UserClaims.Select(y => new UserClaimDocumentModel
                    {
                        Type = y
                    }).ToList()
                }).ToList(),
                UserClaims = resource.UserClaims.Select(x => new UserClaimDocumentModel
                {
                    Type = x
                }).ToList()
            };
        }

        private ApiResource CreatePartialFrom(ProductApiResourceDocumentModel dataEntity)
        {
            return new ApiResource
            {
                Enabled = dataEntity.Enabled,
                Name = dataEntity.Name,
                DisplayName = dataEntity.DisplayName,
                Description = dataEntity.Description,
                ApiSecrets = dataEntity.Secrets.Select(x => new Secret(x.Value, x.Description, x.Expiration)).ToList(),
                UserClaims = dataEntity.UserClaims.Select(x => x.Type).ToList(),
                Scopes = dataEntity.Scopes.Select(x => new Scope
                {
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    Description = x.Description,
                    Required = x.Required,
                    Emphasize = x.Emphasize,
                    ShowInDiscoveryDocument = x.ShowInDiscoveryDocument,
                    UserClaims = x.UserClaims.Select(y => y.Type).ToList()
                }).ToList()
            };
        }
    }
}