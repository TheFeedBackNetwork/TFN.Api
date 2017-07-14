using System;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Repositories.UserAccountAggregate.Document
{
    [CollectionOptions("identity","userAccount")]
    public sealed class UserAccountDocumentModel : UserAccountDocumentModel<Guid>
    {
        public UserAccountDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id, created, modified)
        {

        }
    }
    public class UserAccountDocumentModel<TKey> : BaseDocument<TKey>
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "normalizedUsername")]
        public string NormalizedUsername { get; set; }

        [JsonProperty(PropertyName = "profilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "normalizedEmail")]
        public string NormalizedEmail { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "hashedPassword")]
        public string HashedPassword { get; set; }

        [JsonProperty(PropertyName = "changePasswordKey")]
        public string ChangePasswordKey { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public BiographyDocumentModel Biography { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        public UserAccountDocumentModel(TKey id, DateTime created, DateTime modified)
            : base(id, "userAccount", created, modified)
        {
            
        }
    }

    public class BiographyDocumentModel
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "twitterUrl")]
        public string TwitterUrl { get; set; }

        [JsonProperty(PropertyName = "youtubeUrl")]
        public string YouTubeUrl { get; set; }

        [JsonProperty(PropertyName = "facebookUrl")]
        public string FacebookUrl { get; set; }

        [JsonProperty(PropertyName = "instagramUrl")]
        public string InstagramUrl { get; set; }

        [JsonProperty(PropertyName = "soundcloudUrl")]
        public string SoundCloudUrl { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
    }
}