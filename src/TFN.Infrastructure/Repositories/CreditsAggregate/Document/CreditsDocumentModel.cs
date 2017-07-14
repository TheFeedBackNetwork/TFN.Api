using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.CreditsAggregate.Document
{
    [CollectionOptions("identity", "credits")]
    public sealed class CreditsDocumentModel : CreditsDocumentModel<Guid>
    {
        public CreditsDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id, created, modified)
        {
            
        }
    }
    public class CreditsDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "normalizedUsername")]
        public string NormalizedUsername { get; set; }

        [JsonProperty(PropertyName = "totalCredits")]
        public int TotalCredits { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        public CreditsDocumentModel(TKey id, DateTime created, DateTime modified)
            : base(id,"credits", created,modified)
        {
            
        }
    }
}
