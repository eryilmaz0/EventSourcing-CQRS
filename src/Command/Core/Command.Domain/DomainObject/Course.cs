using Command.Domain.CurrentState;
using Command.Domain.Enum;
using Command.Domain.Event;
using Command.Domain.Exception;

namespace Command.Domain.DomainObject;

public class Course : AggregateRoot
{
    public CourseCurrentState CurrentState { get; set; }

    public Course() 
    {
        CurrentState = new();
        Version = 0;
    }
    
    
    
    protected override void ApplyEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case CourseCreatedDomainEvent courseCreatedEvent : Apply(courseCreatedEvent); break;
            case CourseTitleChangedDomainEvent courseTitleChanged : Apply(courseTitleChanged); break;
            case CourseDescriptionChangedDomainEvent courseDescriptionChanged : Apply(courseDescriptionChanged); break;
            case CoursePrePresentedDomainEvent coursePrePresentedEvent : Apply(coursePrePresentedEvent); break;
            case CourseActivatedDomainEvent courseActivatedEvent : Apply(courseActivatedEvent); break;
            case CourseCompletedDomainEvent courseDisabledEvent : Apply(courseDisabledEvent); break;
            case SectionAppendedDomainEvent sectionAppendedEvent : Apply(sectionAppendedEvent); break;
            case JoinedToCourseDomainEvent joinedToCourseEvent : Apply(joinedToCourseEvent); break;
            case LeftFromCourseDomainEvent leftFromCourseEvent : Apply(leftFromCourseEvent); break;
            case CommentedCourseDomainEvent commentedCourseEvent : Apply(commentedCourseEvent); break;
            default: throw new DomainException("Invalid Event Type.");
        }
    }

    //Command --> Function --> Event
    #region Business Logic

    public void CreateCourse(Guid instructorId, string title, string description, string category)
    {
        IDomainEvent raisedDomainEvent = new CourseCreatedDomainEvent()
        {
            CourseId = Guid.NewGuid(),
            Title = title,
            Description = description,
            Category = category,
            InstructorId = instructorId,
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void ChangeCourseTitle(string title)
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        if (CurrentState.Status == CourseStatus.NonCreated)
            throw new DomainException("Course Status Not Valid For Title Changing.");
         
        
        IDomainEvent raisedDomainEvent = new CourseTitleChangedDomainEvent()
        {
            Title = title,
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void ChangeCourseDescription(string description)
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        if (CurrentState.Status == CourseStatus.NonCreated)
            throw new DomainException("Course Status Not Valid For Description Changing.");
        
        
        IDomainEvent raisedDomainEvent = new CourseDescriptionChangedDomainEvent()
        {
            Description = description,
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void PrePresentationCourse()
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        if (CurrentState.Status != CourseStatus.Created)
            throw new DomainException("Course Status Not Valid For Pre Presenting.");
        
        
        IDomainEvent raisedDomainEvent = new CoursePrePresentedDomainEvent()
        {
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void ActivateCourse()
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        if (CurrentState.Status != CourseStatus.PrePresentation)
            throw new DomainException("Course Status Not Valid For Activating.");
        
        
        IDomainEvent raisedDomainEvent = new CourseActivatedDomainEvent()
        {
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void CompleteCourse()
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        if (CurrentState.Status != CourseStatus.Activated)
            throw new DomainException("Course Status Not Valid For Completing.");
        
        
        IDomainEvent raisedDomainEvent = new CourseCompletedDomainEvent()
        {
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void AddSection(Guid instructorId, string title, string description, string videoUrl)
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        
        if (CurrentState.Status == CourseStatus.Completed || CurrentState.Status == CourseStatus.NonCreated)
            throw new DomainException("Course Status Not Valid For Section Adding.");
        
        
        if(CurrentState.Sections.Count >= 100)
            throw new DomainException("Course Sections is at Limit.");
        

        if (instructorId != CurrentState.InstructorId)
            throw new DomainException("User is not the instructor of this course.");
        
        
        IDomainEvent raisedDomainEvent = new SectionAppendedDomainEvent()
        {
            Title = title,
            Description = description,
            VideoUrl = videoUrl,
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void JoinCourse(Guid participantId)
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        

        if (CurrentState.Participants.Any(x => x.ParticipantId == participantId))
            throw new DomainException("User is already a participant of this course.");
        
        
        IDomainEvent raisedDomainEvent = new JoinedToCourseDomainEvent()
        {
            ParticipantId = participantId,
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void LeaveCourse(Guid participantId)
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        
        if (!CurrentState.Participants.Any(x => x.ParticipantId == participantId))
            throw new DomainException("User is not a participant of this course.");
        
        
        IDomainEvent raisedDomainEvent = new LeftFromCourseDomainEvent()
        {
            ParticipantId = participantId,
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }


    public void CommentCourse(Guid commentorId, string comment)
    {
        if(!IsCourseExist())
            throw new DomainException("Course Not Exist");
        
        
        if (CurrentState.Status != CourseStatus.Activated && CurrentState.Status != CourseStatus.Completed)
            throw new DomainException("Course status is not valid for commenting.");
        
        
        if(!CurrentState.Participants.Any(x => x.ParticipantId == commentorId))
            throw new DomainException("You should be a participant to comment this course.");
        
        IDomainEvent raisedDomainEvent = new CommentedCourseDomainEvent()
        {
            CommentorId = commentorId,
            Comment = comment,
            Created = DateTime.UtcNow
        };
        
        RaiseEvent(raisedDomainEvent);
    }
    

    #endregion

    //F(State, Event) => new State
    #region Projection
    
    private void Apply(CourseCreatedDomainEvent domainEvent)
    {
        AggregateId = domainEvent.CourseId;
        CurrentState.Created = domainEvent.Created;
        CurrentState.InstructorId = domainEvent.InstructorId;
        CurrentState.Title = domainEvent.Title;
        CurrentState.Description = domainEvent.Description;
        CurrentState.Category = domainEvent.Category;
        CurrentState.Status = CourseStatus.Created;
    }
    
    
    private void Apply(CourseTitleChangedDomainEvent domainEvent)
    {
        CurrentState.Title = domainEvent.Title;
    }
    
    
    private void Apply(CourseDescriptionChangedDomainEvent domainEvent)
    {
        CurrentState.Description = domainEvent.Description;
    }
    
    
    
    private void Apply(CoursePrePresentedDomainEvent domainEvent)
    {
        CurrentState.Status = CourseStatus.PrePresentation;
    }
    
    
    private void Apply(CourseActivatedDomainEvent domainEvent)
    {
        CurrentState.Status = CourseStatus.Activated;
    }
    
    
    private void Apply(CourseCompletedDomainEvent domainEvent)
    {
        CurrentState.Status = CourseStatus.Completed;
    }
    
    
    private void Apply(SectionAppendedDomainEvent domainEvent)
    {
        SectionCurrentState newSectionState = new()
        {
            Title = domainEvent.Title, 
            Description = domainEvent.Description, 
            VideoUrl = domainEvent.VideoUrl, 
            Created = domainEvent.Created
        };
        
        CurrentState.Sections.Add(newSectionState);
    }
    
    
    private void Apply(JoinedToCourseDomainEvent domainEvent)
    {
        ParticipantCurrentState newParticipant = new()
        {
            ParticipantId = domainEvent.ParticipantId,
            JoinDate = domainEvent.Created
        };
        
        CurrentState.Participants.Add(newParticipant);
    }
    
    
    private void Apply(LeftFromCourseDomainEvent domainEvent)
    {
        var participant = CurrentState.Participants.First(x => x.ParticipantId == domainEvent.ParticipantId);
        CurrentState.Participants.Remove(participant);
    }
    
    
    private void Apply(CommentedCourseDomainEvent domainEvent)
    {
        CommentCurrentState newComment = new()
        {
            CommentorId = domainEvent.CommentorId,
            Comment = domainEvent.Comment,
            Created = domainEvent.Created
        };
        
        CurrentState.Comments.Add(newComment);
    }
    
    #endregion


    private bool IsCourseExist()
    {
        return CurrentState.Status != CourseStatus.NonCreated;
    }
}