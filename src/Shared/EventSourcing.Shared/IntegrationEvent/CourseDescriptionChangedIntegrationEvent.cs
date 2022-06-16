namespace EventSourcing.Shared.IntegrationEvent;

public class CourseDescriptionChangedIntegrationEvent : IntegrationEvent
{
    public string Description { get; set; }
}