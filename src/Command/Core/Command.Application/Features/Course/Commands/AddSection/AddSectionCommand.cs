using MediatR;

namespace Command.Application.Features.Course.Commands.AddSection;

public class AddSectionCommand : IRequest<AddSectionResponse>
{
    public Guid CourseId { get; set; }
    public Guid InstructorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string VideoUrl { get; set; }
}