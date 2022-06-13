using MediatR;

namespace Command.Application.Features.Course.Commands.JoinCourse;

public class JoinCourseCommand : Abstracts.Command.Command, IRequest<JoinCourseResponse>
{
    public Guid CourseId { get; set; }
    public Guid ParticipantId { get; set; }
}