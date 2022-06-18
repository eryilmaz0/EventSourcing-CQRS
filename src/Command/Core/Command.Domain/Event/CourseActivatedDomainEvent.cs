using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class CourseActivatedDomainEvent : IDomainEvent
{
    public DateTime Created { get; set; }
    
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version)
    {
        return new(aggregateId, EventType.CourseActivated, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId)
    {
        return new CourseActivatedIntegrationEvent()
        {
            AggregateId = aggregateId,
            Created = Created
        };
    }
}