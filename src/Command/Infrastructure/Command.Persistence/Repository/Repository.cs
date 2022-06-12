using Command.Application.Abstracts.Persistence;
using Command.Domain.DomainObject;
using Command.Domain.Event.StoredEvent;

namespace Command.Persistence.Repository;

public class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private readonly IEventStore _eventStore;

    public Repository(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<T> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = (T)Activator.CreateInstance(typeof(T), new object[] { aggregateId });
        var eventStream =  await _eventStore.GetEvents(aggregateId);

        if (eventStream.Any())
        {
            aggregate.PrepareCurrentState(eventStream);
        }

        return aggregate;
    }

    public async Task SaveAsync(T aggregate)
    {
        var events = new List<PersistentEvent>();
        long currentVersion = aggregate.Version;
        
        foreach (var raisedEvent in aggregate.GetRaisedEvents())
        {
            currentVersion++;
            events.Add(raisedEvent.ToPersistentEvent(aggregate.AggregateId, currentVersion));
        }

        await _eventStore.SaveEvents(events, aggregate.Version);
    }
}