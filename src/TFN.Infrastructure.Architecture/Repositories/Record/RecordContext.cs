using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TFN.Infrastructure.Architecture.Records.Attributes;

namespace TFN.Infrastructure.Architecture.Repositories.Record
{
    public class RecordContext
    {
        private CloudTableClient TableClient { get; set; }
        public RecordContext(IOptions<RecordSettings> settings)
        {
            var client = CloudStorageAccount.Parse(settings.Value.ConnectionString);

            TableClient = client.CreateCloudTableClient();
        }

        public async Task<CloudTable> RecordTable<TRecord>()
        {
            if (!typeof(TRecord).GetTypeInfo().IsSealed)
            {
                throw new InvalidOperationException($"Type '{typeof(TRecord).Name}' is not sealed. Sealed document types must be used to represent MongoDB collections as this preserves all fields during serialisation.");
            }

            var options = typeof(TRecord)
                .GetTypeInfo()
                .GetCustomAttributes(false)
                .OfType<RecordOptionsAttribute>()
                .Cast<RecordOptionsAttribute>()
                .SingleOrDefault();

            if (options == null)
            {
                throw new InvalidOperationException($"Type '{typeof(TRecord).Name}' does not have a [CollectionOptions] attribute.");
            }

            var table = TableClient.GetTableReference(options.RecordTableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }
    }
}