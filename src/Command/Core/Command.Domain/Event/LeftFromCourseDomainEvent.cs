﻿using System.Text.Json;
using Command.Domain.Enum;
using Command.Domain.Event.StoredEvent;
using EventSourcing.Shared.IntegrationEvent;

namespace Command.Domain.Event;

public class LeftFromCourseDomainEvent : IDomainEvent
{
    public Guid ParticipantId { get; set; }
    public DateTime Created { get; set; }
    
    public PersistentEvent ToPersistentEvent(Guid aggregateId, long version)
    {
        return new(aggregateId, EventType.LeftFromCourse, version, Created, JsonSerializer.Serialize(this));
    }
 
    public IIntegrationEvent ToIntegrationEvent(Guid aggregateId)
    {
        return new LeftCourseIntegrationEvent()
        {
            AggregateId = aggregateId,
            Created = Created,
            ParticipantId = ParticipantId
        };
    }
    
}