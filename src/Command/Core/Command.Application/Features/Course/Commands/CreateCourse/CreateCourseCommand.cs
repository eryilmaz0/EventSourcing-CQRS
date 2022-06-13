using MediatR;

namespace Command.Application.Features.Course.Commands.CreateCourse;

public class CreateCourseCommand : Abstracts.Command.Command, IRequest<CreateCourseResponse>
{
    public Guid InstructorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    
}