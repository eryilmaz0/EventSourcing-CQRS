using MediatR;

namespace Command.Application.Features.Course.Commands.PrePresentCourse;

public class PrePresentCourseCommand : Abstracts.Command.Command, IRequest<PrePresentCourseResponse>
{
    public Guid CourseId { get; set; }
}