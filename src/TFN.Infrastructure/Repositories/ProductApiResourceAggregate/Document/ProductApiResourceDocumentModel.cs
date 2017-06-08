using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;
using TFN.Infrastructure.Architecture.Documents.Models;

namespace TFN.Infrastructure.Repositories.ProductApiResourceAggregate.Document
{
    [CollectionOptions("productApiResources")]
    public sealed class ProductApiResourceDocumentModel : ProductApiResourceDocumentModel<Guid>
    {
        public ProductApiResourceDocumentModel() { }
        public ProductApiResourceDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id, created, modified)
        {

        }
    }

    public class ProductApiResourceDocumentModel<TKey> : BaseDocument<TKey>
    {
        public ProductApiResourceDocumentModel() { }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; } = true;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "secrets")]
        public ICollection<SecretDocumentModel> Secrets { get; set; } = new List<SecretDocumentModel>();

        [JsonProperty(PropertyName = "scopes")]
        public ICollection<ApiScopeDocumentModel> Scopes { get; set; } = new List<ApiScopeDocumentModel>();

        [JsonProperty(PropertyName = "userClaims")]
        public ICollection<UserClaimDocumentModel> UserClaims { get; set; } = new List<UserClaimDocumentModel>();


        public ProductApiResourceDocumentModel(TKey id, DateTime created, DateTime modified)
            : base(id, "productApiResource", created, modified)
        {

        }
    }

    public class ApiScopeDocumentModel
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool Required { get; set; }

        [JsonProperty(PropertyName = "emphasize")]
        public bool Emphasize { get; set; }

        [JsonProperty(PropertyName = "showInDiscoveryDocument")]
        public bool ShowInDiscoveryDocument { get; set; } = true;

        [JsonProperty(PropertyName = "userClaims")]
        public ICollection<UserClaimDocumentModel> UserClaims { get; set; } = new List<UserClaimDocumentModel>();
    }
}