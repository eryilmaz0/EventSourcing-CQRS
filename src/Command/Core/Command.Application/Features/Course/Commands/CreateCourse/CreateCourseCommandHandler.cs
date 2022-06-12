using Command.Application.Abstracts.Persistence;
using MediatR;


namespace Command.Application.Features.Course.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public CreateCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }
    

    public async Task<CreateCourseResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(Guid.Empty);
        course.CreateCourse(request.InstructorId, request.Title, request.Description, request.Category);
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Course Created."};
    }
}