using System;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.ListenAggregate.Document
{
    public class ListenDocumentMapper : IAggregateMapper<Listen, ListenDocumentModel, Guid>
    {
        public Listen CreateFrom(ListenDocumentModel dataEntity)
        {
            return Listen.Hydrate(
                dataEntity.Id,
                dataEntity.PostId,
                (Listener)Enum.Parse(typeof(Listener), dataEntity.Listener),
                dataEntity.IPAddress,
                dataEntity.Created);
        }

        public ListenDocumentModel CreateFrom(Listen domainEntity)
        {
            return new ListenDocumentModel(domainEntity.Id, domainEntity.Created)
            {
                PostId = domainEntity.PostId,
                Listener = domainEntity.Listener.ToString(),
                IPAddress = domainEntity.IPAddress,
            };
        }
    }
}