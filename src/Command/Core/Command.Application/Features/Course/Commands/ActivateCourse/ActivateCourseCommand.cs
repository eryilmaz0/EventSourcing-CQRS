using MediatR;

namespace Command.Application.Features.Course.Commands.ActivateCourse;

public class ActivateCourseCommand : Abstracts.Command.Command, IRequest<ActivateCourseResponse>
{
    public Guid CourseId { get; set; }
}