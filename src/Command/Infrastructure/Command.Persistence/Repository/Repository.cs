using Command.Application.Abstracts.Persistence;
using Command.Domain.DomainObject;
using Command.Domain.Event.StoredEvent;
using Command.Persistence.Utilities;

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
        var eventStream =  await _eventStore.GetEventsAsync(aggregateId);

        if (eventStream.Any())
        {
            //Projection
            var deserializedEvents = EventConverter.DeserializePersistentEvents(eventStream);
            aggregate.PrepareCurrentState(deserializedEvents);
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

        await _eventStore.SaveEventsAsync(events, aggregate.Version);
    }
}