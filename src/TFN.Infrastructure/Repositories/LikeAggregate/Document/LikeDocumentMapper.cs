using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.LikeAggregate.Document
{
    public class LikeDocumentMapper : IAggregateMapper<Like, LikeDocumentModel, Guid>
    {
        public Like CreateFrom(LikeDocumentModel dataEntity)
        {
            throw new NotImplementedException();
        }

        public LikeDocumentModel CreateFrom(Like domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}