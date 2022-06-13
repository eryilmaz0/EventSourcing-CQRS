using MediatR;

namespace Command.Application.Features.Course.Commands.ChangeCourseDescription;

public class ChangeCourseDescriptionCommand : Abstracts.Command.Command, IRequest<ChangeCourseDescriptionResponse>
{
    public Guid CourseId { get; set; }
    public string Description { get; set; }
}