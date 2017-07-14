using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.CreditsAggregate.Document
{
    public class CreditsDocumentMapper : IAggregateMapper<Credits, CreditsDocumentModel, Guid>
    {
        public Credits CreateFrom(CreditsDocumentModel dataEntity)
        {
            return Credits.Hydrate(
                dataEntity.Id,
                dataEntity.UserId,
                dataEntity.Username,
                dataEntity.NormalizedUsername,
                dataEntity.TotalCredits,
                dataEntity.Created,
                dataEntity.Modified,
                dataEntity.IsActive);
        }

        public CreditsDocumentModel CreateFrom(Credits domainEntity)
        {
            return new CreditsDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Modified)
            {
                UserId =  domainEntity.UserId,
                Username = domainEntity.Username,
                NormalizedUsername = domainEntity.NormalizedUsername,
                TotalCredits = domainEntity.TotalCredits,
                IsActive = domainEntity.IsActive
            };
        }
    }
}