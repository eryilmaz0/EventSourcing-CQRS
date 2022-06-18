using Command.Domain.Event;

namespace Command.Domain.DomainObject;

public abstract class AggregateRoot
{
    
    private ICollection<IDomainEvent> _events = new List<IDomainEvent>();
    public long Version { get; protected set; }
    public Guid AggregateId { get; protected set; }
    

    public ICollection<IDomainEvent> RaisedEvents() => _events;

    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
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