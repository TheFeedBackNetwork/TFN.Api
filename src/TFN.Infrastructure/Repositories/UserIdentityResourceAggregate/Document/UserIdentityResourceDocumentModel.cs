using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;
using TFN.Infrastructure.Architecture.Documents.Models;

namespace TFN.Infrastructure.Repositories.UserIdentityResourceAggregate.Document
{
    [CollectionOptions("configuration","userIdentityResource")]
    public sealed class UserIdentityResourceDocumentModel : UserIdentityResourceDocumentModel<Guid>
    {
        public UserIdentityResourceDocumentModel() { }
        public UserIdentityResourceDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id, created, modified)
        {

        }
    }

    public class UserIdentityResourceDocumentModel<TKey> : BaseDocument<TKey>
    {
        public UserIdentityResourceDocumentModel() { }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; } = true;

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

        public UserIdentityResourceDocumentModel(TKey id, DateTime created, DateTime modified)
            : base(id, "userIdentityResource", created, modified)
        {

        }
    }
}