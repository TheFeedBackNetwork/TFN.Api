using System;
using System.IO;
using System.Threading.Tasks;
using TFN.Infrastructure.Interfaces.Components;

namespace TFN.Infrastructure.Components.Storage
{
    public class S3StorageComponent : IS3StorageComponent
    {
        public S3StorageComponent()
        {

        }
        public Task Delete(string container, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<Uri> Upload(Stream trackStream, string container, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}