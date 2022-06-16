namespace EventSourcing.Shared.IntegrationEvent;

public class CourseCommentedIntegrationEvent : IntegrationEvent
{
    public string Comment { get; set; }
}