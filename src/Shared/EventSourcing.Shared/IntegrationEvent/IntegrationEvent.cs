namespace EventSourcing.Shared.IntegrationEvent;

public class IntegrationEvent : IIntegrationEvent
{
    public Guid AggregateId { get; set; }
    public DateTime Created { get; set; }
}