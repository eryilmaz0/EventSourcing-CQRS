using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.LeaveCourse;

public class LeaveCourseCommandHandler : IRequestHandler<LeaveCourseCommand, LeaveCourseResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public LeaveCourseCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<LeaveCourseResponse> Handle(LeaveCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.LeaveCourse(request.ParticipantId);
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Participant left from Course."};
    }
}