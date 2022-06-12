using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.ActivateCourse;

public class ActivateCourseCommandHandler : IRequestHandler<ActivateCourseCommand, ActivateCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public ActivateCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<ActivateCourseResponse> Handle(ActivateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.ActivateCourse();
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Course Activated."};
    }
}