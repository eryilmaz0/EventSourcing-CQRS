using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.PrePresentCourse;

public class PrePresentCourseCommandHandler : IRequestHandler<PrePresentCourseCommand, PrePresentCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public PrePresentCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<PrePresentCourseResponse> Handle(PrePresentCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.PrePresentationCourse();
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Course Pre Presented."};
    }
}