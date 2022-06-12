using Command.Application.Abstracts.Persistence;
using Command.Domain.Event;
using Command.Domain.Event.StoredEvent;
using Command.Persistence.Context;
using Command.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Command.Persistence.EventStore;

public class EfEventStore : IEventStore
{
    private readonly EventSourcingDbContext _context;

    public EfEventStore(EventSourcingDbContext context)
    {
        _context = context;
    }

    
    public async Task SaveEvents(IEnumerable<PersistentEvent> events, long expectedVersion)
    {
        //TODO : do version check for consistency
        await _context.Events.AddRangeAsync(events);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<IEvent>> GetEvents(Guid aggregateId)
    {
        var persistentEvents = await _context.Events.Where(x => x.AggregateId == aggregateId).ToListAsync();
        return EventConverter.DeserializePersistentEvents(persistentEvents);
    }
}