using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.ScoreAggregate.Document
{
    public class ScoreDocumentMapper : IAggregateMapper<Score, ScoreDocumentModel, Guid>
    {
        public Score CreateFrom(ScoreDocumentModel dataEntity)
        {
            throw new NotImplementedException();
        }

        public ScoreDocumentModel CreateFrom(Score domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}