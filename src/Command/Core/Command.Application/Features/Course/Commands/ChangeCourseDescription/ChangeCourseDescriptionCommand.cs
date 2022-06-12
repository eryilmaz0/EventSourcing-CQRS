using MediatR;

namespace Command.Application.Features.Course.Commands.ChangeCourseDescription;

public class ChangeCourseDescriptionCommand : IRequest<ChangeCourseDescriptionResponse>
{
    public Guid CourseId { get; set; }
    public string Description { get; set; }
}