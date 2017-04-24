using System;

namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public class DocumentDbSettings
    {
        public string DatabaseName { get; set; }
        public string DatabaseKey { get; set; }
        public Uri DatabaseUri { get; set; }
    }
}
