namespace TFN.Infrastructure.Architecture.Records
{
    public interface IRecord<out TKey>
    {
        TKey Id { get; }
    }
}