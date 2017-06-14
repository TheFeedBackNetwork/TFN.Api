using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.TransientUserAggregate.Document
{
    public class TransientUserAccountDocumentMapper : IAggregateMapper<TransientUserAccount, TransientUserAccountDocumentModel, Guid>
    {
        public TransientUserAccount CreateFrom(TransientUserAccountDocumentModel dataEntity)
        {
            return TransientUserAccount.Hydrate(
                dataEntity.Id,
                dataEntity.Username,
                dataEntity.NormalizedUsername,
                dataEntity.Email,
                dataEntity.NormalizedEmail,
                dataEntity.VerificationKey,
                dataEntity.Created,
                dataEntity.Modified);
        }

        public TransientUserAccountDocumentModel CreateFrom(TransientUserAccount domainEntity)
        {
            return new TransientUserAccountDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Modified)
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