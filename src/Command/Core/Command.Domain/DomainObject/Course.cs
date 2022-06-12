using Command.Domain.CurrentState;
using Command.Domain.Enum;
using Command.Domain.Event;
using Command.Domain.Exception;

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
        switch (@event)
        {
            case CourseCreatedEvent courseCreatedEvent : Apply(courseCreatedEvent); break;
            case CourseTitleChangedEvent courseTitleChanged : Apply(courseTitleChanged); break;
            case CourseDescriptionChangedEvent courseDescriptionChanged : Apply(courseDescriptionChanged); break;
            case CoursePrePresentedEvent coursePrePresentedEvent : Apply(coursePrePresentedEvent); break;
            case CourseActivatedEvent courseActivatedEvent : Apply(courseActivatedEvent); break;
            case CourseCompletedEvent courseDisabledEvent : Apply(courseDisabledEvent); break;
            case SectionAppendedEvent sectionAppendedEvent : Apply(sectionAppendedEvent); break;
            case JoinedToCourseEvent joinedToCourseEvent : Apply(joinedToCourseEvent); break;
            case LeftFromCourseEvent leftFromCourseEvent : Apply(leftFromCourseEvent); break;
            case CommentedCourseEvent commentedCourseEvent : Apply(commentedCourseEvent); break;
            default: throw new DomainException("Invalid Event Type.");
        }
    }

    public override void PrepareCurrentState(ICollection<IEvent> events)
    {
        //Projecting every event, and reaching current state and version number.
        foreach (var @event in events)
        {
            ApplyEvent(@event);
            Version++;
        }
    }


    //Command --> Function --> Event
    #region Business Logic

    public void CreateCourse(Guid instructorId, string title, string description, string category)
    {
        if (CurrentState.Status != CourseStatus.NonCreated)
        {
            throw new DomainException("Course Already Created.");
        }

        IEvent raisedEvent = new CourseCreatedEvent()
        {
            Title = title,
            Description = description,
            Category = category,
            InstructorId = instructorId,
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void ChangeCourseTitle(string title)
    {
        if (CurrentState.Status == CourseStatus.NonCreated)
        {
            throw new DomainException("Course Status Not Valid For Title Changing.");
        } 
        
        IEvent raisedEvent = new CourseTitleChangedEvent()
        {
            Title = title,
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void ChangeCourseDescription(string description)
    {
        if (CurrentState.Status == CourseStatus.NonCreated)
        {
            throw new DomainException("Course Status Not Valid For Description Changing.");
        } 
        
        IEvent raisedEvent = new CourseDescriptionChangedEvent()
        {
            Description = description,
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void PrePresentationCourse()
    {
        if (CurrentState.Status != CourseStatus.Created)
        {
            throw new DomainException("Course Status Not Valid For Pre Presenting.");
        }
        
        IEvent raisedEvent = new CoursePrePresentedEvent()
        {
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void ActivateCourse()
    {
        if (CurrentState.Status != CourseStatus.PrePresentation)
        {
            throw new DomainException("Course Status Not Valid For Activating.");
        }
        
        IEvent raisedEvent = new CourseActivatedEvent()
        {
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void CompleteCourse()
    {
        if (CurrentState.Status != CourseStatus.Actived)
        {
            throw new DomainException("Course Status Not Valid For Completing.");
        }
        
        IEvent raisedEvent = new CourseCompletedEvent()
        {
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void AddSection(Guid instructorId, string title, string description, string videoUrl)
    {
        if (CurrentState.Status == CourseStatus.Completed || CurrentState.Status == CourseStatus.NonCreated)
            throw new DomainException("Course Status Not Valid For Section Adding.");
        
        
        if(CurrentState.Sections.Count >= 100)
            throw new DomainException("Course Sections is at Limit.");

        if (instructorId != CurrentState.InstructorId)
            throw new DomainException("User is not the instructor of this course.");
        
        
        IEvent raisedEvent = new SectionAppendedEvent()
        {
            Title = title,
            Description = description,
            VideoUrl = videoUrl,
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void JoinCourse(Guid participantId)
    {
        if (CurrentState.Status == CourseStatus.Completed || CurrentState.Status == CourseStatus.NonCreated)
            throw new DomainException("Course is not joinable.");

        if (CurrentState.Participants.Any(x => x.ParticipantId == participantId))
            throw new DomainException("User is already a participant of this course.");
        
        
        IEvent raisedEvent = new JoinedToCourseEvent()
        {
            ParticipantId = participantId,
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void LeaveCourse(Guid participantId)
    {
        if (!CurrentState.Participants.Any(x => x.ParticipantId == participantId))
            throw new DomainException("User is not a participant of this course.");
        
        IEvent raisedEvent = new LeftFromCourseEvent()
        {
            ParticipantId = participantId,
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }


    public void CommentCourse(Guid commentorId, string comment)
    {
        if (CurrentState.Status != CourseStatus.Actived || CurrentState.Status != CourseStatus.Completed)
            throw new DomainException("Course status is not valid for commenting.");
        
        if(!CurrentState.Participants.Any(x => x.ParticipantId == commentorId))
            throw new DomainException("You should be a participant to comment this course.");
        
        IEvent raisedEvent = new CommentedCourseEvent()
        {
            CommentorId = commentorId,
            Comment = comment,
            Created = DateTime.UtcNow
        };
        
        ApplyEvent(raisedEvent);
        AddRaisedEvent(raisedEvent);
    }
    

    #endregion

    //F(State, Event) => new State
    #region Projection
    
    private void Apply(CourseCreatedEvent @event)
    {
        CurrentState.Created = @event.Created;
        CurrentState.InstructorId = @event.InstructorId;
        CurrentState.Title = @event.Title;
        CurrentState.Description = @event.Description;
        CurrentState.Category = @event.Category;
        CurrentState.Status = CourseStatus.Created;
    }
    
    
    private void Apply(CourseTitleChangedEvent @event)
    {
        CurrentState.Title = @event.Title;
    }
    
    
    private void Apply(CourseDescriptionChangedEvent @event)
    {
        CurrentState.Description = @event.Description;
    }
    
    
    
    private void Apply(CoursePrePresentedEvent @event)
    {
        CurrentState.Status = CourseStatus.PrePresentation;
    }
    
    
    private void Apply(CourseActivatedEvent @event)
    {
        CurrentState.Status = CourseStatus.Actived;
    }
    
    
    private void Apply(CourseCompletedEvent @event)
    {
        CurrentState.Status = CourseStatus.Completed;
    }
    
    
    private void Apply(SectionAppendedEvent @event)
    {
        SectionCurrentState newSectionState = new()
        {
            Title = @event.Title, 
            Description = @event.Description, 
            VideoUrl = @event.VideoUrl, 
            Created = @event.Created
        };
        
        CurrentState.Sections.Add(newSectionState);
    }
    
    
    private void Apply(JoinedToCourseEvent @event)
    {
        ParticipantCurrentState newParticipant = new()
        {
            ParticipantId = @event.ParticipantId,
            JoinDate = @event.Created
        };
        
        CurrentState.Participants.Add(newParticipant);
    }
    
    
    private void Apply(LeftFromCourseEvent @event)
    {
        var participant = CurrentState.Participants.First(x => x.ParticipantId == @event.ParticipantId);
        CurrentState.Participants.Remove(participant);
    }
    
    
    private void Apply(CommentedCourseEvent @event)
    {
        CommentCurrentState newComment = new()
        {
            CommentorId = @event.CommentorId,
            Comment = @event.Comment,
            Created = @event.Created
        };
        
        CurrentState.Comments.Add(newComment);
    }
    
    #endregion
}