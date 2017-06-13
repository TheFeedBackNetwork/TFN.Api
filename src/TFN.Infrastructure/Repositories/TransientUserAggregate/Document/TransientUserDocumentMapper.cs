using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.TransientUserAggregate.Document
{
    public class TransientUserDocumentMapper : IAggregateMapper<TransientUser, TransientUserDocumentModel, Guid>
    {
        public TransientUser CreateFrom(TransientUserDocumentModel dataEntity)
        {
            return TransientUser.Hydrate(
                dataEntity.Id,
                dataEntity.Username,
                dataEntity.NormalizedUsername,
                dataEntity.Email,
                dataEntity.NormalizedEmail,
                dataEntity.VerificationKey,
                dataEntity.Created,
                dataEntity.Modified);
        }

        public TransientUserDocumentModel CreateFrom(TransientUser domainEntity)
        {
            return new TransientUserDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Modified)
            {
                Email = domainEntity.Email,
                NormalizedEmail = domainEntity.NormalizedEmail,
                Username = domainEntity.Username,
                NormalizedUsername = domainEntity.NormalizedUsername,
                VerificationKey = domainEntity.VerificationKey
            };
        }
    }
}