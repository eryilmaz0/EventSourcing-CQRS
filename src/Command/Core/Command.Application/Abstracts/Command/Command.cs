namespace Command.Application.Abstracts.Command;

public abstract class Command
{
    //For Log Tracing
    public Guid TrackId { get; set; }
}