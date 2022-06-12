using Command.Application.Abstracts.Persistence;
using MediatR;

namespace Command.Application.Features.Course.Commands.AddSection;

public class AddSectionCommandHandler : IRequestHandler<AddSectionCommand, AddSectionResponse>
{
    private readonly IRepository<Domain.DomainObject.Course> _repository;

    public AddSectionCommandHandler(IRepository<Domain.DomainObject.Course> repository)
    {
        _repository = repository;
    }

    public async Task<AddSectionResponse> Handle(AddSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.CourseId);
        course.AddSection(request.InstructorId, request.Title, request.Description, request.VideoUrl);
        await _repository.SaveAsync(course);
        
        //Send Integration Event To EventBus
        return new(){IsSuccess = true, ResultMessage = "Section Added Into Course."};
    }
}