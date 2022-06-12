using Command.Domain.Event;

namespace Command.Domain.DomainObject;

public abstract class AggregateRoot
{
    
    private ICollection<IEvent> _events = new List<IEvent>();
    public long Version { get; protected set; }
    public Guid AggregateId { get; }
    

    public ICollection<IEvent> GetRaisedEvents() => _events;
    protected void AddRaisedEvent(IEvent @event) => _events.Add(@event);
    
    
    protected abstract void ApplyEvent(IEvent @event);
    public abstract void PrepareCurrentState(IEnumerable<IEvent> events);


    public AggregateRoot(Guid id)
    {
        AggregateId = id;
        Version = 0;
    }
}