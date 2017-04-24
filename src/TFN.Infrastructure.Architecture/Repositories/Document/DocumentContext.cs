﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using TFN.Infrastructure.Architecture.Documents.Attributes;

namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public class DocumentContext
    {
        public DocumentClient DocumentClient { get; private set; }
        public string DatabaseName { get; private set; }
        public DocumentContext(IOptions<DocumentDbSettings> settings)
        {
            DatabaseName = settings.Value.DatabaseName;
            DocumentClient = new DocumentClient(settings.Value.DatabaseUri, settings.Value.DatabaseKey, new ConnectionPolicy
            {
                MaxConnectionLimit = 100,
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            });

            DocumentClient.OpenAsync().Wait();
            CreateDatabaseIdNotExist().Wait();
        }

        private async Task CreateDatabaseIdNotExist()
        {
            try
            {
                await DocumentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseName));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DocumentClient.CreateDatabaseAsync(new Database { Id = DatabaseName });
                }
                else
                {
                    throw;
                }
            }
        }

        public IDocumentCollection<TDocument> Collection<TDocument>()
        {
            if (!typeof(TDocument).GetTypeInfo().IsSealed)
            {
                throw new InvalidOperationException($"Type '{typeof(TDocument).Name}' is not sealed. Sealed document types must be used to represent MongoDB collections as this preserves all fields during serialisation.");
            }

            var options = typeof(TDocument)
                .GetTypeInfo()
                .GetCustomAttributes(false)
                .OfType<CollectionOptionsAttribute>()
                .Cast<CollectionOptionsAttribute>()
                .SingleOrDefault();

            if (options == null)
            {
                throw new InvalidOperationException($"Type '{typeof(TDocument).Name}' does not have a [CollectionOptions] attribute.");
            }
            return new DocumentCollection<TDocument>(DocumentClient, DatabaseName, options.CollectionName);
        }
    }
}