namespace EventSourcing.Shared.IntegrationEvent;

public class SectionAppendedIntegrationEvent : IntegrationEvent
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string VideoUrl { get; set; }
}