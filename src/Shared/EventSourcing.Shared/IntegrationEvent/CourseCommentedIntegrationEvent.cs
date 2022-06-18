namespace EventSourcing.Shared.IntegrationEvent;

public class CourseCommentedIntegrationEvent : IntegrationEvent
{
    public Guid CommentorId { get; set; }
    public string Comment { get; set; }
}