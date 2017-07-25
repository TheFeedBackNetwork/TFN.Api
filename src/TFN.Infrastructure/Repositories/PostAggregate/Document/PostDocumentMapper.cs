using System;
using System.Linq;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.PostAggregate.Document
{
    public class PostDocumentMapper : IAggregateMapper<Post, PostDocumentModel, Guid>
    {
        public Post CreateFrom(PostDocumentModel dataEntity)
        {
            return Post.Hydrate(
                dataEntity.Id,
                dataEntity.UserId,
                dataEntity.TrackUrl,
                dataEntity.Text,
                (Genre)Enum.Parse(typeof(Genre), dataEntity.Genre),
                dataEntity.Tags?.ToList().AsReadOnly(),
                dataEntity.IsActive,
                dataEntity.Created,
                dataEntity.Modified);
        }

        public PostDocumentModel CreateFrom(Post domainEntity)
        {
            return new PostDocumentModel(domainEntity.Id, domainEntity.UserId, domainEntity.Text,
                domainEntity.IsActive, domainEntity.Created, domainEntity.Modified)
            {
                TrackUrl = domainEntity.TrackUrl,
                Tags = domainEntity.Tags?.ToList(),
                Genre = domainEntity.Genre.ToString(),
            };
        }
    }
}