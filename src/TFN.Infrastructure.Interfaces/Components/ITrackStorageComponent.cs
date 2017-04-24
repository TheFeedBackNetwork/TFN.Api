using System;
using System.IO;
using System.Threading.Tasks;

namespace TFN.Infrastructure.Interfaces.Components
{
    public interface ITrackStorageComponent
    {
        Task<Uri> UploadUnprocessed(Stream trackStream, string fileName);
        Task<Uri> UploadProcessed(Stream trackStream, string fileName);
        Task<Uri> UploadProcessed(string path, string fileName);
        Task DeleteLocal(string filePath);
    }
}