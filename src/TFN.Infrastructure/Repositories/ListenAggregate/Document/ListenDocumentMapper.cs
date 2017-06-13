using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.ListenAggregate.Document
{
    public class ListenDocumentMapper : IAggregateMapper<Listen, ListenDocumentModel, Guid>
    {
        public Listen CreateFrom(ListenDocumentModel dataEntity)
        {
            throw new NotImplementedException();
        }

        public ListenDocumentModel CreateFrom(Listen domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}