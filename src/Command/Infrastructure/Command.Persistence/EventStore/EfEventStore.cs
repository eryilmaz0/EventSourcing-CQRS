﻿using Command.Application.Abstracts.Persistence;
using Command.Application.Exception;
using Command.Domain.Event;
using Command.Domain.Event.StoredEvent;
using Command.Domain.Exception;
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
        var aggregateId = events.First().AggregateId;
        
        //Check the latest event version and aggregate version are matching  
        if (events.Max(x => x.Version) != expectedVersion)
            throw new ConsistencyException();
        
        await _context.Events.AddRangeAsync(events);
        await _context.SaveChangesAsync();
    }

    
    public async Task<IEnumerable<PersistentEvent>> GetEventsAsync(Guid aggregateId)
    {
        return await _context.Events
                                                     .Where(x => x.AggregateId == aggregateId)
                                                     .OrderBy(x => x.Version)
                                                     .ToListAsync();
    }
    
    
}