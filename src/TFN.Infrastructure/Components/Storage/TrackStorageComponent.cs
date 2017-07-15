using System;
using System.IO;
using System.Threading.Tasks;
using TFN.Infrastructure.Interfaces.Components;

namespace TFN.Infrastructure.Components.Storage
{
    public class TrackStorageComponent : ITrackStorageComponent
    {
        public IBlobStorageComponent BlobStorageComponent { get; private set; }
        public IS3StorageComponent S3StorageComponent { get; private set; }
        const string UnprocessedContainer = "unprocessedtracks";
        const string ProcessedContainer = "processedtracks";
        public TrackStorageComponent(IBlobStorageComponent blobStorageComponent, IS3StorageComponent s3StorageComponent)
        {
            BlobStorageComponent = blobStorageComponent;
            S3StorageComponent = s3StorageComponent;

        }
        public async Task<Uri> UploadProcessed(Stream trackStream, string fileName)
        {
            return await BlobStorageComponent.Upload(trackStream, ProcessedContainer, fileName);
        }

        public async Task<Uri> UploadProcessed(string path, string fileName)
        {
            var stream = File.Open(path, FileMode.Open);
            return await BlobStorageComponent.Upload(stream, ProcessedContainer, fileName);
        }

        public async Task<Uri> UploadUnprocessed(Stream trackStream, string fileName)
        {
            return await BlobStorageComponent.Upload(trackStream, UnprocessedContainer, fileName);
        }

        public Task DeleteLocal(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return Task.CompletedTask;
        }
    }
}
