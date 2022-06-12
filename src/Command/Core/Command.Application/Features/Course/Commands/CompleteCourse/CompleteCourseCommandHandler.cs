using Command.Application.Abstracts.CommandHandler;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.CompleteCourse;

public class CompleteCourseCommandHandler : CommandHandler, IRequestHandler<CompleteCourseCommand, CompleteCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;
    private readonly IEventBus _eventBus;

    public CompleteCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository, IEventBus eventBus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    
    public async Task<CompleteCourseResponse> Handle(CompleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.CompleteCourse();
        await _repository.SaveAsync(course);
        
        var integrationEvents = PrepareIntegrationEvents(course);
        await _eventBus.PublishEventsAsync(integrationEvents);
        
        return new(){IsSuccess = true, ResultMessage = "Course Completed."};
    }
}