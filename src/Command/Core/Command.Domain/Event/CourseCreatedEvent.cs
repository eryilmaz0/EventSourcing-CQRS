using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class CourseCreatedEvent : IEvent
{
    public Guid InstructorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Created { get; set; }
    
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version)
    {
        return new(aggregateId, EventType.CourseCreatedEvent, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId, long version)
    {
        return new FakeIntegrationEvent();
    }
}