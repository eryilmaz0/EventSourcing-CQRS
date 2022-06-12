﻿using Command.Application.Abstracts.Persistence;
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

    
    public async Task SaveEventsAsync(IEnumerable<PersistentEvent> events, long expectedVersion)
    {
        //TODO : do version check for consistency
        await _context.Events.AddRangeAsync(events);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<IEvent>> GetEventsAsync(Guid aggregateId)
    {
        var persistentEvents = await _context.Events
                                                     .Where(x => x.AggregateId == aggregateId)
                                                     .OrderBy(x => x.Version)
                                                     .ToListAsync();
        
        return EventConverter.DeserializePersistentEvents(persistentEvents);
    }
}