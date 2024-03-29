﻿using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class SectionAppendedDomainEvent : IDomainEvent
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string VideoUrl { get; set; }
    public DateTime Created { get; set; }
    
    
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version)
    {
         return new(aggregateId, EventType.SectionAppended, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId)
    {
        return new SectionAppendedIntegrationEvent()
        {
            AggregateId = aggregateId,
            Created = Created,
            Title = Title,
            Description = Description,
            VideoUrl = VideoUrl
        };
    }
}