﻿namespace EventSourcing.Shared.IntegrationEvent;

public interface IIntegrationEvent
{
    public Guid AggregateId { get; set; }
    public DateTime Created { get; set; }
}