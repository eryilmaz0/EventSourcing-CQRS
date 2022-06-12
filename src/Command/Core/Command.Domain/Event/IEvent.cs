using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public interface IEvent
{
    public PersistentEvent<Guid> ToPersistentEvent(Guid aggregateId, long version);
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId, long version);
}