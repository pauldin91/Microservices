namespace Ordering.Domain.Interfaces
{
    public interface IAggregate : IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents(); 
    }
    public interface IAggregate<T> : IAggregate , IEntity<T>
    { 
    }
}