namespace EventSourcing.Shared.IntegrationEvent;

public class LeftCourseIntegrationEvent : IntegrationEvent
{
    public Guid ParticipantId { get; set; }
}