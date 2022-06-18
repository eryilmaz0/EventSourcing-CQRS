using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class CourseDescriptionChangedDomainEvent : IDomainEvent
{
    public string Description { get; set; }
    public DateTime Created { get; set; }
    
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version)
    {
        return new(aggregateId, EventType.CourseDescriptionChanged, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId)
    {
        return new CourseDescriptionChangedIntegrationEvent()
        {
            AggregateId = aggregateId,
            Created = Created,
            Description = Description
        };
    }
}