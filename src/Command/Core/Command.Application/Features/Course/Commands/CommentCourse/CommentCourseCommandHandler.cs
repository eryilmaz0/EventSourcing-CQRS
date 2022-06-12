using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.CommentCourse;

public class CommentCourseCommandHandler : IRequestHandler<CommentCourseCommand, CommentCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public CommentCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<CommentCourseResponse> Handle(CommentCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.CommentCourse(request.CommentorId, request.Comment);
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Participant Commented Course."};
    }
}