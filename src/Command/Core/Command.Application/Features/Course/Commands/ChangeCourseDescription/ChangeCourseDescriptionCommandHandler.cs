using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.ChangeCourseDescription;

public class ChangeCourseDescriptionCommandHandler : IRequestHandler<ChangeCourseDescriptionCommand, ChangeCourseDescriptionResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public ChangeCourseDescriptionCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<ChangeCourseDescriptionResponse> Handle(ChangeCourseDescriptionCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.ChangeCourseDescription(request.Description);
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Course Description Changed."};
    }
}