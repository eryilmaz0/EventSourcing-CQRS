using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class CourseActivatedEvent : IEvent
{
    public PersistentEvent<Guid> ToPersistentEvent(Guid aggregateId)
    {
        throw new NotImplementedException();
    }

    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId)
    {
        throw new NotImplementedException();
    }
}