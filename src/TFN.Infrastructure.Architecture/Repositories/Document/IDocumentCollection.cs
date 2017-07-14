using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public interface IDocumentCollection<TDocument>
        where TDocument : class
    { 
        Task<TDocument> Find(string id);
        Task<TDocument> Find(Expression<Func<TDocument, bool>> predicate);
        Task<IEnumerable<TDocument>> FindAll();
        Task<IEnumerable<TDocument>> FindAll(Expression<Func<TDocument, bool>> predicate);
        Task<IEnumerable<TDocument>> FindAllPaginated(string continuationToken);
        Task<IEnumerable<TDocument>> FindAllPaginated(Expression<Func<TDocument, bool>> predicate, string continuationToken);
        Task<IEnumerable<TDocument>> FindAllPaginated(Expression<Func<TDocument, bool>> wherePredicate, Expression<Func<TDocument, dynamic>> orderPredicate , string continuationToken);
        Task Add(TDocument document);
        Task Update(TDocument document, string id);
        Task Delete(string id);
        Task<int> Count(Expression<Func<TDocument, bool>> predicate);
        Task<bool> Any();
        Task<bool> Any(Expression<Func<TDocument, bool>> predicate);
        IQueryable<TDocument> CreateQuery(FeedOptions feedOptions);
        IQueryable<TDocument> CreateQuery(SqlQuerySpec sqlQuery, FeedOptions feedOptions);
    }
}