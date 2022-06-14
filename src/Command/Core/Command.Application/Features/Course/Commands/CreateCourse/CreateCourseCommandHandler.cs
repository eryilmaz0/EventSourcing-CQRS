using Command.Application.Abstracts.CommandHandler;
using Command.Application.Abstracts.Infrastructure;
using Command.Application.Abstracts.Persistence;
using MediatR;


namespace Command.Application.Features.Course.Commands.CreateCourse;

public class CreateCourseCommandHandler : CommandHandler, IRequestHandler<CreateCourseCommand, CreateCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;
    private readonly IEventBus _eventBus;

    public CreateCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository, IEventBus eventBus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }
    

    public async Task<CreateCourseResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        Guid newCourseId = Guid.NewGuid();
        var course = await _repository.GetByIdAsync(newCourseId);
        course.CreateCourse(request.InstructorId, request.Title, request.Description, request.Category);
        await _repository.SaveAsync(course);
        
        var integrationEvents = PrepareIntegrationEvents(course);
        await _eventBus.PublishEventsAsync(integrationEvents);
        
        return new(){IsSuccess = true, ResultMessage = "Course Created.", CourseId = newCourseId};
    }
}