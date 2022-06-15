using Command.Domain.Event;
using Command.Domain.Event.StoredEvent;

namespace Command.Application.Abstracts.Persistence;

public interface IEventStore
{
    public Task SaveEventsAsync(IEnumerable<PersistentEvent> events, long expectedVersion);
    public Task<IEnumerable<PersistentEvent>> GetEventsAsync(Guid aggregateId);
}