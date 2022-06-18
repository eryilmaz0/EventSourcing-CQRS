using EventSourcing.Shared.IntegrationEvent;
using FirstQueryProject.Abstract.Projection;
using FirstQueryProject.ReadModel;
using MassTransit;

namespace FirstQueryProject.Consumer;

public class EventConsumer : IConsumer<CourseCreatedIntegrationEvent>,
                             IConsumer<CourseTitleChangedIntegrationEvent>,
                             IConsumer<CourseDescriptionChangedIntegrationEvent>,
                             IConsumer<CoursePresentedIntegrationEvent>,
                             IConsumer<CourseActivatedIntegrationEvent>,
                             IConsumer<CourseCompletedIntegrationEvent>,
                             IConsumer<SectionAppendedIntegrationEvent>,
                             IConsumer<JoinedCourseIntegrationEvent>,
                             IConsumer<LeftCourseIntegrationEvent>,
                             IConsumer<CourseCommentedIntegrationEvent>

{
    
    private readonly IProjectionBuilder<Course> _courseProjectionBuilder;

    
    public EventConsumer(IProjectionBuilder<Course> courseProjectionBuilder)
    {
        _courseProjectionBuilder = courseProjectionBuilder;
    }
    
    
    
    public async Task Consume(ConsumeContext<CourseCreatedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<CourseTitleChangedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<CourseDescriptionChangedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<CoursePresentedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<CourseActivatedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<CourseCompletedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<SectionAppendedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<JoinedCourseIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<LeftCourseIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }

    public async Task Consume(ConsumeContext<CourseCommentedIntegrationEvent> context)
    {
        await _courseProjectionBuilder.ProjectModelAsync(context.Message);
    }
}