using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Logging;
using TFN.Infrastructure.Interfaces.Components;


namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public class DocumentCollection<TDocument> : IDocumentCollection<TDocument>
        where TDocument : class, new()
    {
        public const int maxItems = 20;
        public DocumentClient DocumentClient { get; private set; }
        public Uri CollectionUri { get; private set; }
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }
        public string DocumentType { get; private set; }
        public ILogger Logger { get; private set; }
        public IQueryCursorComponent QueryCursorComponent { get; private set; }
        public DocumentCollection(DocumentClient documentClient, ILogger logger,IQueryCursorComponent queryCursorComponent,  string databaseName, string collectionName, string documentType)
        {
            Logger = logger;
            DocumentClient = documentClient;
            CollectionName = collectionName;
            DocumentType = documentType;
            DatabaseName = databaseName;
            QueryCursorComponent = queryCursorComponent;

            CollectionUri = GetCollectionLink();
            
            CreateCollectionIfNotExist().Wait();
        }

        private async Task CreateCollectionIfNotExist()
        {
            try
            {
                await this.DocumentClient.ReadDocumentCollectionAsync(GetCollectionLink());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DocumentClient.CreateDocumentCollectionAsync(
                        GetDatabaseLink(),
                        new DocumentCollection { Id = CollectionName },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        private Uri GetDatabaseLink()
        {
            return UriFactory.CreateDatabaseUri(DatabaseName);
        }
        private Uri GetCollectionLink()
        {
            return UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
        }
        private Uri GetDocumentLink(string id)
        {
            return UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id);
        }

        public async Task<TDocument> Find(string id)
        {

            try
            {
                var query = await DocumentClient.ReadDocumentAsync<TDocument>(GetDocumentLink(id));
                var document = query.Document;

                return document;
            }
            catch (Exception)
            {
                return default(TDocument);
            }

        }

        public async Task<TDocument> Find(Expression<Func<TDocument, bool>> predicate)
        {
            try
            {
                var query = DocumentClient.CreateDocumentQuery<TDocument>(CollectionUri)
                    .Where(predicate)
                    .Take(1)
                    .Select(x => x)
                    .AsDocumentQuery();

                var execution = await query.ExecuteNextAsync<TDocument>();

                var result = execution.FirstOrDefault();

                return result;
            }
            catch (Exception e)
            {
                Logger.LogCritical(e.ToString());
                return default(TDocument);
            }
        }

        public async Task<IEnumerable<TDocument>> FindAll()
        {
            var options = new FeedOptions
            {
                //MaxItemCount = maxItems,
                //RequestContinuation = continuationToken
            };
            var hasMoreResults = true;
            var list = new List<TDocument>();

            while (hasMoreResults)
            {
                var query = DocumentClient.CreateDocumentQuery<TDocument>(CollectionUri, options).AsDocumentQuery();
                string nextCursor = null;

                if (query.HasMoreResults)
                {
                    var result = await query.ExecuteNextAsync<TDocument>();
                    nextCursor = result.ResponseContinuation;
                    QueryCursorComponent.SetCursor(nextCursor);
                    list.AddRange(result);
                }

                hasMoreResults = nextCursor != null;
            }
            

            return list;
        }

        public async Task<IEnumerable<TDocument>> FindAll(Expression<Func<TDocument, bool>> predicate)
        {
            var options = new FeedOptions
            {
                //MaxItemCount = maxItems,
                //RequestContinuation = continuationToken
            };
            var hasMoreResults = true;
            var list = new List<TDocument>();

            while (hasMoreResults)
            {
                var query = DocumentClient
                    .CreateDocumentQuery<TDocument>(CollectionUri, options)
                    .Where(predicate)
                    .Select(x => x)
                    .AsDocumentQuery();

                string nextCursor = null;

                if (query.HasMoreResults)
                {
                    var result = await query.ExecuteNextAsync<TDocument>();
                    nextCursor = result.ResponseContinuation;
                    QueryCursorComponent.SetCursor(nextCursor);
                    list.AddRange(result);
                }

                hasMoreResults = nextCursor != null;
            }


            return list;
        }

        public async Task<IEnumerable<TDocument>> FindAllPaginated(string continuationToken)
        {
            var options = new FeedOptions
            {
                MaxItemCount = maxItems,
                RequestContinuation = continuationToken
            };

            var query = DocumentClient.CreateDocumentQuery<TDocument>(CollectionUri, options).AsDocumentQuery();

            var list = new List<TDocument>();

            if (query.HasMoreResults)
            {
                var result = await query.ExecuteNextAsync<TDocument>();
                var nextCursor = result.ResponseContinuation;
                QueryCursorComponent.SetCursor(nextCursor);
                list.AddRange(result);
            }

            return list;
        }

        public async Task<IEnumerable<TDocument>> FindAllPaginated(Expression<Func<TDocument, bool>> predicate, string continuationToken)
        {
            var options = new FeedOptions
            {
                MaxItemCount = maxItems,
                RequestContinuation = continuationToken
            };

            var query = DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri,options)
                .Where(predicate)
                .Select(x => x)
                .AsDocumentQuery();
            
            
            var list = new List<TDocument>();

            if (query.HasMoreResults)
            {
                var result = await query.ExecuteNextAsync<TDocument>();
                var nextCursor = result.ResponseContinuation;
                QueryCursorComponent.SetCursor(nextCursor);
                list.AddRange(result);
            }

            return list;
        }

        public async Task<IEnumerable<TDocument>> FindAllPaginated(Expression<Func<TDocument, bool>> wherePredicate, Expression<Func<TDocument, dynamic>> orderPredicate, string continuationToken)
        {
            var options = new FeedOptions
            {
                MaxItemCount = maxItems,
                RequestContinuation = continuationToken
            };

            var query = DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri, options)
                .Where(wherePredicate)
                .Select(x => x)
                .OrderBy(orderPredicate)
                .AsDocumentQuery();


            var list = new List<TDocument>();

            if (query.HasMoreResults)
            {
                var result = await query.ExecuteNextAsync<TDocument>();
                var nextCursor = result.ResponseContinuation;
                QueryCursorComponent.SetCursor(nextCursor);
                list.AddRange(result);
            }

            return list;
        }

        public async Task<int> Count(Expression<Func<TDocument, bool>> predicate)
        {
            var options = new FeedOptions
            {
                //MaxItemCount = maxItems,
                //RequestContinuation = continutationToken
            };

            var count = await DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri, options)
                .Where(predicate)
                .CountAsync();
            
            return count;
        }

        public async Task<bool> Any()
        {
            var options = new FeedOptions
            {
                //MaxItemCount = maxItems,
                //RequestContinuation = continutationToken
            };

            var query = DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri, options)
                .AsDocumentQuery();

            var list = new List<TDocument>();

            if (query.HasMoreResults)
            {
                var result = await query.ExecuteNextAsync<TDocument>();
                var nextCursor = result.ResponseContinuation;
                QueryCursorComponent.SetCursor(nextCursor);
                list.AddRange(result);
            }

            return list.Count > 0;
        }

        public async Task<bool> Any(Expression<Func<TDocument, bool>> predicate)
        {
            var options = new FeedOptions
            {
                //MaxItemCount = maxItems,
                //RequestContinuation = continutationToken
            };

            var query = DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri, options)
                .Where(predicate)
                .Select(x => x)
                .AsDocumentQuery();

            var list = new List<TDocument>();

            if (query.HasMoreResults)
            {
                var result = await query.ExecuteNextAsync<TDocument>();
                var nextCursor = result.ResponseContinuation;
                QueryCursorComponent.SetCursor(nextCursor);
                list.AddRange(result);
            }

            return list.Count > 0;
        }

        public async Task Add(TDocument document)
        {
            await DocumentClient.CreateDocumentAsync(CollectionUri, document);
        }

        public async Task Update(TDocument document, string id)
        {
            //need to TDocument update this modified to DateTime.UtcNow
            await DocumentClient.ReplaceDocumentAsync(GetDocumentLink(id), document);
        }

        public async Task Upsert(TDocument document)
        {
            await DocumentClient.UpsertDocumentAsync(GetCollectionLink(), document);
        }

        public async Task Delete(string id)
        {
            await DocumentClient.DeleteDocumentAsync(GetDocumentLink(id));
        }

        public IQueryable<TDocument> CreateQuery(FeedOptions feedOptions)
        {
            return DocumentClient.CreateDocumentQuery<TDocument>(CollectionUri, feedOptions);
        }

        public IQueryable<TDocument> CreateQuery(SqlQuerySpec sqlQuery, FeedOptions feedOptions)
        {
            return DocumentClient.CreateDocumentQuery<TDocument>(CollectionUri, sqlQuery, feedOptions);
        }

        
    }


}