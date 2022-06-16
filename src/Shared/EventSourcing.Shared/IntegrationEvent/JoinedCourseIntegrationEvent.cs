namespace EventSourcing.Shared.IntegrationEvent;

public class JoinedCourseIntegrationEvent : IntegrationEvent
{
    public Guid ParticipantId { get; set; }
}