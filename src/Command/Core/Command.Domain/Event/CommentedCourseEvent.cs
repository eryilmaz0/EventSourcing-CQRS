using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class CommentedCourseEvent : IEvent
{
    public Guid CommentorId { get; set; }
    public string Comment { get; set; }
    public DateTime Created { get; set; }
    
    
    public PersistentEvent<Guid> ToPersistentEvent(Guid aggregateId, long version)
    {
        return new(aggregateId, EventType.CommentedCourse, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId, long version)
    {
        throw new NotImplementedException();
    }
}