using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.CommentAggregate.Document
{
    public class CommentDocumentMapper : IAggregateMapper<Comment, CommentDocumentModel, Guid>
    {
        public Comment CreateFrom(CommentDocumentModel dataEntity)
        {
            throw new NotImplementedException();
        }

        public CommentDocumentModel CreateFrom(Comment domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}