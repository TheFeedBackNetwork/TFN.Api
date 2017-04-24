namespace TFN.Infrastructure.Architecture.Documents
{
    public interface IDocument<out TKey>
    {
        TKey Id { get; }
    }
}