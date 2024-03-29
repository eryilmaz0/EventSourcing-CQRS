﻿using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class CoursePrePresentedDomainEvent : IDomainEvent
{
    public DateTime Created { get; set; }
    
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version)
    {
        return new(aggregateId, EventType.CoursePrePresented, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId)
    {
        return new CoursePresentedIntegrationEvent()
        {
            AggregateId = aggregateId,
            Created = Created,
        };
    }
}