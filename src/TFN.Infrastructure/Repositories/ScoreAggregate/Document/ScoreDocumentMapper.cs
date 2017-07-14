using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.ScoreAggregate.Document
{
    public class ScoreDocumentMapper : IAggregateMapper<Score, ScoreDocumentModel, Guid>
    {
        public Score CreateFrom(ScoreDocumentModel dataEntity)
        {
            return Score.Hydrate(
                dataEntity.Id,
                dataEntity.CommentId,
                dataEntity.UserId,
                dataEntity.Username,
                dataEntity.Created);
        }

        public ScoreDocumentModel CreateFrom(Score domainEntity)
        {
            return new ScoreDocumentModel(domainEntity.Id, domainEntity.Created)
            {
                CommentId = domainEntity.CommentId,
                UserId = domainEntity.UserId,
                Username = domainEntity.Username
            };
        }
    }
}