namespace TFN.Infrastructure.Architecture.Documents
{
    public interface IDocument<TKey>
    {
        TKey Id { get; set; }

        string Type { get; set; }
    }
}