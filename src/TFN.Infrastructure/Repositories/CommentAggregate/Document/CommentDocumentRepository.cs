using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.CommentAggregate.Document
{
    public class CommentDocumentRepository : DocumentRepository<Comment, CommentDocumentModel, Guid>, ICommentRepository
    {
        public CommentDocumentRepository(
            IAggregateMapper<Comment, CommentDocumentModel, Guid> mapper, DocumentContext context,
            ILogger<CommentDocumentRepository> logger)
            : base(mapper, context, logger)
        {
            
        }

        public async Task<IReadOnlyList<Comment>> FindCommentsPaginated(Guid postId, string continuationToken)
        {
            var documents = await Collection.FindAllPaginated(x => x.PostId == postId, continuationToken);

            if (documents == null)
            {
                return null;
            }

            var aggregates = documents.Select(Mapper.CreateFrom);

            return aggregates?.ToList();
        }

        public async Task<int> Count(Guid postId)
        {
            var count = await Collection.Count(x => x.PostId == postId);

            return count;
        }


        //SCORE

        public Task Add(Score entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid commentId, Guid scoreId)
        {
            throw new NotImplementedException();
        }

        public Task<Score> Find(Guid commentId, Guid scoreId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Score>> FindAllScores(Guid comentId, string continuationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CommentSummary> FindCommentScoreSummary(Guid commentId, int limit, string username)
        {
            throw new NotImplementedException();
        }

        
    }
}