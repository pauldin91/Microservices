using MediatR;

namespace Ordering.Domain.Interfaces
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();

        public DateTime OccurredOn => DateTime.UtcNow;

        public string EventType => GetType().AssemblyQualifiedName;
    }
}