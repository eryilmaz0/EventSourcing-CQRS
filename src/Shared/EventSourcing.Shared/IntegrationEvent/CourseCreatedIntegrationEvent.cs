namespace EventSourcing.Shared.IntegrationEvent;

public class CourseCreatedIntegrationEvent : IntegrationEvent
{
    public Guid InstructorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}