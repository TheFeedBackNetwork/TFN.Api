using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public interface IDocumentCollection<TDocument>
    {
        Task<TDocument> Find(string id);
        Task<TDocument> Find(Func<TDocument, bool> predicate);
        Task<IEnumerable<TDocument>> FindAll();
        Task<IEnumerable<TDocument>> FindAll(Func<TDocument, bool> predicate);
        Task Add(TDocument document);
        Task Update(TDocument document, string id);
        Task Delete(string id);
        IQueryable<TDocument> CreateQuery(FeedOptions feedOptions);
        IQueryable<TDocument> CreateQuery(SqlQuerySpec sqlQuery, FeedOptions feedOptions);
    }
}