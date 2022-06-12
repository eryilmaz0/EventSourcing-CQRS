using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.JoinCourse;

public class JoinCourseCommandHandler : IRequestHandler<JoinCourseCommand, JoinCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public JoinCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<JoinCourseResponse> Handle(JoinCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.JoinCourse(request.ParticipantId);
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Participant Joined to Course."};
    }
}