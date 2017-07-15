using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.TransientUserAccountAggregate.Document
{
    [CollectionOptions("identity", "transientUserAccount")]
    public sealed class TransientUserAccountDocumentModel : TransientUserAccountDocumentModel<Guid>
    {
        public TransientUserAccountDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id,created,modified)
        {

        }

        public TransientUserAccountDocumentModel()
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
            : base(id,"transientUserAccount",created,modified)
        {
            
        }

        public TransientUserAccountDocumentModel()
        {

        }
    }
}