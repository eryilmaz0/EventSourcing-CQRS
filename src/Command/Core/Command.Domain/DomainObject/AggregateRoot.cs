using Command.Domain.Event;

namespace Command.Domain.DomainObject;

public abstract class AggregateRoot
{
    
    private IDictionary<long, IDomainEvent> _events = new Dictionary<long, IDomainEvent>();
    public long Version { get; protected set; }
    public Guid AggregateId { get; protected set; }
    

    public IDictionary<long, IDomainEvent> RaisedEvents() => _events;

    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        Version++;
        _events.Add(Version, domainEvent);
        ApplyEvent(domainEvent);
    } 
    
    public void ReBuild(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyEvent(@event);
            Version++;
        }
    }
    
    
    protected abstract void ApplyEvent(IDomainEvent domainEvent);
    
}