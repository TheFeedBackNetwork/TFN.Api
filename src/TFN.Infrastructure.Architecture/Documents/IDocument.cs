using System;

namespace TFN.Infrastructure.Architecture.Documents
{
    public interface IDocument<TKey>
    {
        TKey Id { get; set; }
        string Type { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
    }
}