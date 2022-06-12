using MediatR;

namespace Command.Application.Features.Course.Commands.LeaveCourse;

public class LeaveCourseCommand : IRequest<LeaveCourseResponse>
{
    public Guid CourseId { get; set; }
    public Guid ParticipantId { get; set; }
}