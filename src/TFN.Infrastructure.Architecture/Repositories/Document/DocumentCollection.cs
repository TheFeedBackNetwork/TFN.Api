using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public class DocumentCollection<TDocument> : IDocumentCollection<TDocument>
    {
        public DocumentClient DocumentClient { get; private set; }
        public Uri CollectionUri { get; private set; }
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }
        public DocumentCollection(DocumentClient documentClient, string databaseName, string collectionName)
        {
            DocumentClient = documentClient;
            CollectionName = collectionName;
            DatabaseName = databaseName;
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
         
            //I hardly comment however I think the generic arity needs to be changed for this so that
            //TDocument can be figured out during  compile time... for now this will do. It cheats 
            //the documentdb query but does not cheat the repository since the type at runtime is known.

            var query = await DocumentClient
                .ReadDocumentAsync<dynamic>(GetDocumentLink(id));
            
            //var document = (TDocument) query.Document;

            return default(TDocument);
            //return document;

        }

        public Task<TDocument> Find(Func<TDocument, bool> predicate)
        {

            var query = DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri)
                .Where(predicate)
                .Select(x => x)
                .SingleOrDefault();


            return Task.FromResult(query);
        }

        public Task<IEnumerable<TDocument>> FindAll()
        {
            var query = DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri)
                .Select(x => x);

            IEnumerable<TDocument> result = query.ToList();

            return Task.FromResult(result);
        }

        public Task<IEnumerable<TDocument>> FindAll(Func<TDocument, bool> predicate)
        {

            var query = DocumentClient
                .CreateDocumentQuery<TDocument>(CollectionUri)
                .Where(predicate)
                .Select(x => x);


            return Task.FromResult(query);
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