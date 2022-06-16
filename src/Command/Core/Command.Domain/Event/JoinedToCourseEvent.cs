using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class JoinedToCourseEvent : IEvent
{
    public Guid ParticipantId { get; set; }
    public DateTime Created { get; set; }
    
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version)
    {
        return new(aggregateId, EventType.JoinedToCourse, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId)
    {
        return new JoinedCourseIntegrationEvent()
        {
            AggregateId = aggregateId,
            Created = Created,
            ParticipantId = ParticipantId
        };
    }
}