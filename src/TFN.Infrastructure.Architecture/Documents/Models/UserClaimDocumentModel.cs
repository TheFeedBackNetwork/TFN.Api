using Newtonsoft.Json;

namespace TFN.Infrastructure.Architecture.Documents.Models
{
    public class UserClaimDocumentModel
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}