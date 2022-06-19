using Command.Application.Abstracts.Persistence;
using Command.Domain.DomainObject;
using Command.Domain.Event.StoredEvent;
using Command.Persistence.Utilities;

namespace Command.Persistence.Repository;

public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
{
    private readonly IEventStore _eventStore;

    public Repository(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<T> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new T();
        var eventStream =  await _eventStore.GetEventsAsync(aggregateId);

        if (eventStream.Any())
        {
            //Projection
            var deserializedEvents = EventConverter.DeserializePersistentEvents(eventStream);
            aggregate.ReBuild(deserializedEvents);
        }

        return aggregate;
    }

    
    public async Task SaveAsync(T aggregate)
    {
        var events = new List<PersistentEvent>();

        foreach (var raisedEvent in aggregate.RaisedEvents())
        {
            events.Add(raisedEvent.Value.ToPersistentEvent(aggregate.AggregateId, raisedEvent.Key));
        }

        await _eventStore.SaveEventsAsync(events, aggregate.Version);
    }
}