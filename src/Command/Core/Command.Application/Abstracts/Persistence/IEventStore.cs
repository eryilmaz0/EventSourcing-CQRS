using Command.Domain.Event;
using Command.Domain.Event.StoredEvent;

namespace Command.Application.Abstracts.Persistence;

public interface IEventStore
{
    public Task SaveEvents(IEnumerable<PersistentEvent> events, long expectedVersion);
    public Task<IEnumerable<IEvent>> GetEvents(Guid aggregateId);
}