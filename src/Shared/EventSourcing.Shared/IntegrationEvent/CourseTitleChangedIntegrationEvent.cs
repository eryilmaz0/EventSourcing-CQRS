namespace EventSourcing.Shared.IntegrationEvent;

public class CourseTitleChangedIntegrationEvent : IntegrationEvent
{
    public string Title { get; set; }
}