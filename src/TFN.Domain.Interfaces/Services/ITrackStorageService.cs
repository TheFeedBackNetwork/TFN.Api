using System;
using System.IO;
using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITrackStorageService
    {
        Task<Uri> UploadUnprocessed(Stream trackStream, string fileName);
        Task<Uri> UploadProcessed(Stream trackStream, string fileName);
        Task<Uri> UploadProcessed(string path, string fileName);
        Task DeleteLocal(string filePath);
    }
}
