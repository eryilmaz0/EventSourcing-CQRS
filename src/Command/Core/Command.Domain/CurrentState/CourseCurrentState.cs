using Command.Domain.Enum;

namespace Command.Domain.CurrentState;

public class CourseCurrentState : CurrentState
{
    public Guid InstructorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CourseStatus Status { get; set; }
    public string Category { get; set; }
    public DateTime? Created { get; set; }
    
    public ICollection<SectionCurrentState> Sections { get; set; }
    public ICollection<CommentCurrentState> Comments { get; set; }
    public ICollection<ParticipantCurrentState> Participants { get; set; }


    public CourseCurrentState()
    {
        Status = CourseStatus.NonCreated;
        Sections = new List<SectionCurrentState>();
        Comments = new List<CommentCurrentState>();
        Participants = new List<ParticipantCurrentState>();
    }
}


public class SectionCurrentState
{
    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string VideoUrl { get; set; }
    public DateTime Created { get; set; }


    public SectionCurrentState()
    {
        Id = Guid.NewGuid();
    }
}


public class CommentCurrentState
{
    public Guid CommentorId { get; set; }
    public string Comment { get; set; }
    public DateTime Created { get; set; }
}


public class ParticipantCurrentState
{
    public Guid ParticipantId { get; set; }
    public DateTime JoinDate { get; set; }
}