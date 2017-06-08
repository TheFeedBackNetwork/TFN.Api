using System;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.UserAccountAggregate.Document
{
    public class UserAccountDocumentMapper : IAggregateMapper<UserAccount, UserAccountDocumentModel, Guid>
    {
        public UserAccount CreateFrom(UserAccountDocumentModel dataEntity)
        {
            return UserAccount.Hydrate(
                dataEntity.Id,
                dataEntity.Username,
                dataEntity.NormalizedUsername,
                dataEntity.HashedPassword,
                dataEntity.ProfilePictureUrl,
                dataEntity.Email,
                dataEntity.NormalizedEmail,
                dataEntity.FullName,
                CreateFromPartial(dataEntity.Biography),
                dataEntity.Created,
                dataEntity.Modified,
                dataEntity.IsActive);
        }

        public UserAccountDocumentModel CreateFrom(UserAccount domainEntity)
        {
            return new UserAccountDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Modified)
            {
                Biography = new BiographyDocumentModel
                {
                    FacebookUrl = domainEntity.Biography.FacebookUrl,
                    InstagramUrl = domainEntity.Biography.InstagramUrl,
                    Location = domainEntity.Biography.Location,
                    SoundCloudUrl = domainEntity.Biography.SoundCloudUrl,
                    Text = domainEntity.Biography.Text,
                    TwitterUrl = domainEntity.Biography.TwitterUrl,
                    YouTubeUrl = domainEntity.Biography.YouTubeUrl
                },
                Username = domainEntity.Username,
                NormalizedUsername = domainEntity.NormalizedUsername,
                Email = domainEntity.Email,
                NormalizedEmail = domainEntity.NormalizedEmail,
                FullName = domainEntity.FullName,
                HashedPassword = domainEntity.HashedPassword,
                IsActive = domainEntity.IsActive
            };
        }

        private Biography CreateFromPartial(BiographyDocumentModel biography)
        {
            return Biography.From(
                biography.Text,
                biography.InstagramUrl,
                biography.SoundCloudUrl,
                biography.TwitterUrl,
                biography.TwitterUrl,
                biography.FacebookUrl,
                biography.Location);
        }
    }
}