using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.CompleteCourse;

public class CompleteCourseCommandHandler : IRequestHandler<CompleteCourseCommand, CompleteCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public CompleteCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<CompleteCourseResponse> Handle(CompleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.CompleteCourse();
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Course Completed."};
    }
}