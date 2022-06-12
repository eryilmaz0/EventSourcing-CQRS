using Command.Application.Abstracts.CommandHandler;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.JoinCourse;

public class JoinCourseCommandHandler : CommandHandler, IRequestHandler<JoinCourseCommand, JoinCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;
    private readonly IEventBus _eventBus;

    public JoinCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository, IEventBus eventBus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }
    

    public async Task<JoinCourseResponse> Handle(JoinCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.JoinCourse(request.ParticipantId);
        await _repository.SaveAsync(course);
        
        var integrationEvents = PrepareIntegrationEvents(course);
        await _eventBus.PublishEventsAsync(integrationEvents);
        
        return new(){IsSuccess = true, ResultMessage = "Participant Joined to Course."};
    }
}