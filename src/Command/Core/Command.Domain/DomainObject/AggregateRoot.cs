using Command.Domain.Event;

namespace Command.Domain.DomainObject;

public abstract class AggregateRoot
{
    private ICollection<IEvent> _events = new List<IEvent>();
    protected long Version { get; set; }
    protected Guid AggregateId { get; }
    

    public ICollection<IEvent> GetRaisedEvents() => _events;
    protected void AddRaisedEvent(IEvent @event) => _events.Add(@event);
    
    
    protected abstract void ApplyEvent(IEvent @event);
    public abstract void PrepareCurrentState(ICollection<IEvent> events);


    public AggregateRoot(Guid id)
    {
        AggregateId = id;
        Version = 0;
    }
}