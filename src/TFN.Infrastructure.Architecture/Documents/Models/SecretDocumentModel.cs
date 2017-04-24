using System;
using IdentityServer4;
using Newtonsoft.Json;

namespace TFN.Infrastructure.Architecture.Documents.Models
{
    public class SecretDocumentModel
    {
        [JsonProperty(PropertyName = "description")]
        public  string Description { get; set; }

        [JsonProperty(PropertyName = "value")]
        public  string Value { get; set; }

        [JsonProperty(PropertyName = "expiration")]
        public  DateTime? Expiration { get; set; }

        [JsonProperty(PropertyName = "type")]
        public  string Type { get; set; } = IdentityServerConstants.SecretTypes.SharedSecret;
    }
}