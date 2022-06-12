using Command.Application.Abstracts.CommandHandler;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.PrePresentCourse;

public class PrePresentCourseCommandHandler : CommandHandler, IRequestHandler<PrePresentCourseCommand, PrePresentCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;
    private readonly IEventBus _eventBus;

    public PrePresentCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository, IEventBus eventBus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    
    public async Task<PrePresentCourseResponse> Handle(PrePresentCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.PrePresentationCourse();
        await _repository.SaveAsync(course);
        
        var integrationEvents = PrepareIntegrationEvents(course);
        await _eventBus.PublishEventsAsync(integrationEvents);
        
        return new(){IsSuccess = true, ResultMessage = "Course Pre Presented."};
    }
}