using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.LikeAggregate.Document
{
    public class LikeDocumentMapper : IAggregateMapper<Like, LikeDocumentModel, Guid>
    {
        public Like CreateFrom(LikeDocumentModel dataEntity)
        {
            return Like.Hydrate(
                dataEntity.Id,
                dataEntity.PostId,
                dataEntity.UserId,
                dataEntity.Username,
                dataEntity.Created);
        }

        public LikeDocumentModel CreateFrom(Like domainEntity)
        {
            return new LikeDocumentModel(domainEntity.Id, domainEntity.Created)
            {
                PostId = domainEntity.PostId,
                Username = domainEntity.Username,
                UserId = domainEntity.UserId,
            };
        }
    }
}