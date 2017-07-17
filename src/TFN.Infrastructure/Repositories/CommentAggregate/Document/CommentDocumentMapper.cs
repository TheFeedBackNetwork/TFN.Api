using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.CommentAggregate.Document
{
    public class CommentDocumentMapper : IAggregateMapper<Comment, CommentDocumentModel, Guid>
    {
        public Comment CreateFrom(CommentDocumentModel dataEntity)
        {
            return Comment.Hydrate(
                dataEntity.Id,
                dataEntity.UserId,
                dataEntity.PostId,
                dataEntity.Text,
                dataEntity.IsActive,
                dataEntity.Created,
                dataEntity.Modified);
        }

        public CommentDocumentModel CreateFrom(Comment domainEntity)
        {
            return new CommentDocumentModel(domainEntity.Id, domainEntity.UserId,
                domainEntity.Text,
                domainEntity.IsActive, domainEntity.Created, domainEntity.Modified)
            {
                PostId = domainEntity.PostId
            };
        }
    }
}