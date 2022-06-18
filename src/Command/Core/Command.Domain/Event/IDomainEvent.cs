using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public interface IDomainEvent
{
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version);
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId);
}