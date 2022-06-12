using Command.Domain.Enum;

namespace Command.Domain.Event.StoredEvent;

public class PersistentEvent
{
    public Guid AggregateId { get; }
    public EventType Type { get; }
    public long Version { get; }
    public DateTime Created { get; }
    public string Payload { get; }


    public PersistentEvent(Guid aggregateId, EventType eventType, long version, DateTime created, string payload)
    {
        AggregateId = aggregateId;
        Type = eventType;
        Version = version;
        Created = created;
        Payload = payload;
    }
}