﻿using EventSourcing.Shared.IntegrationEvent;
using SecondQueryProject.Abstract.Projection;
using SecondQueryProject.Abstract.Repository;
using SecondQueryProject.ReadModel;

namespace SecondQueryProject.Projection;

public class CourseProjectionBuilder : IProjectionBuilder<Course>
{
    private readonly IRepository<Course> _repository;

    public CourseProjectionBuilder(IRepository<Course> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    
    public async Task ProjectModelAsync(IIntegrationEvent @event)
    {
        switch (@event)
        {
            case CourseCreatedIntegrationEvent courseCreatedEvent : await ApplyEvent(courseCreatedEvent); break; 
            case CourseTitleChangedIntegrationEvent courseTitleChangedEvent : await ApplyEvent(courseTitleChangedEvent); break; 
            case CourseDescriptionChangedIntegrationEvent courseDescriptionChangedEvent :await ApplyEvent(courseDescriptionChangedEvent); break; 
            case CoursePresentedIntegrationEvent coursePresentedEvent : await ApplyEvent(coursePresentedEvent); break; 
            case CourseActivatedIntegrationEvent courseActivatedEvent : await ApplyEvent(courseActivatedEvent); break; 
            case CourseCompletedIntegrationEvent courseCompletedEvent : await ApplyEvent(courseCompletedEvent); break; 
            case SectionAppendedIntegrationEvent sectionAppendedEvent : await ApplyEvent(sectionAppendedEvent); break; 
            case JoinedCourseIntegrationEvent joinedCourseEvent : await ApplyEvent(joinedCourseEvent); break; 
            case LeftCourseIntegrationEvent leftCourseEvent : await ApplyEvent(leftCourseEvent); break; 
            case CourseCommentedIntegrationEvent courseCommentedEvent : await ApplyEvent(courseCommentedEvent); break; 
            default: throw new InvalidOperationException("Undefined Integration Event.");
        }
    }


    private async Task ApplyEvent(CourseCreatedIntegrationEvent @event)
    {
        var course = new Course()
        {
            AggregateId = @event.AggregateId,
            InstructorId = @event.InstructorId,
            Title = @event.Title,
            Description = @event.Description,
            Category = @event.Category,
            Created = @event.Created,
            Status = "Created"
        };

        await _repository.InsertAsync(course);
    }
    
    private async Task ApplyEvent(CourseTitleChangedIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Title = @event.Title;
        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(CourseDescriptionChangedIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Description = @event.Description;
        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(CoursePresentedIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Status = "Presenting";
        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(CourseActivatedIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Status = "Activated";
        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(CourseCompletedIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Status = "Completed";
        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(SectionAppendedIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Sections.Add(new()
        {
            Title = @event.Title,
            Description = @event.Description,
            VideoUrl = @event.VideoUrl,
            Created = @event.Created
        });

        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(JoinedCourseIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Participants.Add(new()
        {
            Id = @event.ParticipantId,
            Created = @event.Created
        });

        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(LeftCourseIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        var participant = course.Participants.First(x => x.Id == @event.ParticipantId);
        course.Participants.Remove(participant);
        await _repository.UpdateAsync(course);
    }
    
    
    private async Task ApplyEvent(CourseCommentedIntegrationEvent @event)
    {
        var course = await _repository.GetAsync(x => x.AggregateId == @event.AggregateId);
        course.Comments.Add(new()
        {
            CommentorId = @event.CommentorId,
            Created = @event.Created,
            Content = @event.Comment
        });

        await _repository.UpdateAsync(course);
    }
}