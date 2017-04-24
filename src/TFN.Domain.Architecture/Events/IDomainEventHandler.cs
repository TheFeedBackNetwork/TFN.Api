namespace TFN.Domain.Architecture.Events
{
    public interface IDomainEventHandler<T>
        where T : IDomainEvent
    {
        void Handle(T args);
    }
}