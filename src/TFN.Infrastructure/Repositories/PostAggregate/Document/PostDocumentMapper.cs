using System;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.PostAggregate.Document
{
    public class PostDocumentMapper : IAggregateMapper<Post, PostDocumentModel, Guid>
    {
        public Post CreateFrom(PostDocumentModel dataEntity)
        {
            throw new NotImplementedException();
        }

        public PostDocumentModel CreateFrom(Post domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}