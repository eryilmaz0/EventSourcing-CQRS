using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.ChangeCourseTitle;

public class ChangeCourseTitleCommandHandler : IRequestHandler<ChangeCourseTitleCommand, ChangeCourseTitleResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public ChangeCourseTitleCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<ChangeCourseTitleResponse> Handle(ChangeCourseTitleCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.ChangeCourseTitle(request.Title);
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Course Title Changed."};
    }
}