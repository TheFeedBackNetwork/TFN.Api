﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using TFN.Infrastructure.Interfaces.Components;

namespace TFN.Infrastructure.Components.Storage
{
    public class BlobStorageComponent : IBlobStorageComponent
    {
        public CloudBlobClient BlobClient { get; private set; }
        public BlobStorageComponent(IConfiguration configuration)
        {
            BlobClient = CloudStorageAccount
                .Parse(configuration["Storage:StorageAccountConnectionString"])
                .CreateCloudBlobClient();
        }
        public async Task Delete(string container, string fileName)
        {
            var blobContainer = BlobClient.GetContainerReference(container);

            var block = blobContainer.GetBlockBlobReference(fileName);

            await block.DeleteAsync();
        }

        public async Task<Uri> Upload(Stream trackStream, string container, string fileName)
        {
            trackStream.Position = 0;

            var blobContainer = BlobClient.GetContainerReference(container);

            await blobContainer.CreateIfNotExistsAsync();

            var block = blobContainer.GetBlockBlobReference(fileName);

            block.UploadFromStream(trackStream);

            trackStream.Close();
            trackStream.Dispose();

            return block.Uri;
        }
    }
}