using Command.Domain.CurrentState;
using Command.Domain.Event;

namespace Command.Domain.DomainObject;

public class Course : AggregateRoot
{
    protected CourseCurrentState CurrentState { get; set; }

    public Course(Guid id) : base(id)
    {
        CurrentState = new();
    }
    
    
    
    protected override void ApplyEvent(IEvent @event)
    {
        //Apply event
        AddRaisedEvent(@event);
    }

    public override void PrepareCurrentState(ICollection<IEvent> events)
    {
        //Make Projection for each event, and increase version

    }
}