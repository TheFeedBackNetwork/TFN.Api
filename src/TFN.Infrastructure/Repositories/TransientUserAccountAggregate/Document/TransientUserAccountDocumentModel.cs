using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;

namespace TFN.Infrastructure.Repositories.TransientUserAggregate.Document
{
    public sealed class TransientUserAccountDocumentModel : TransientUserAccountDocumentModel<Guid>
    {
        public TransientUserAccountDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id,created,modified)
        {

        }
    }

    public class TransientUserAccountDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "normalizedUsername")]
        public string NormalizedUsername { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "normalizeEmail")]
        public string NormalizedEmail { get; set; }

        [JsonProperty(PropertyName = "verificationKey")]
        public string VerificationKey { get; set; }

        public TransientUserAccountDocumentModel(TKey id, DateTime created, DateTime modified)
            : base(id,"transientUser",created,modified)
        {
            
        }
    }
}