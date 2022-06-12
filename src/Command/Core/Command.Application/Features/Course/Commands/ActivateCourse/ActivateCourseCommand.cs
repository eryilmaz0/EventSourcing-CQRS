using MediatR;

namespace Command.Application.Features.Course.Commands.ActivateCourse;

public class ActivateCourseCommand : IRequest<ActivateCourseResponse>
{
    public Guid CourseId { get; set; }
}