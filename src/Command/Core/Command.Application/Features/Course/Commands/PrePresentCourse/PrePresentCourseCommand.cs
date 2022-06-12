using MediatR;

namespace Command.Application.Features.Course.Commands.PrePresentCourse;

public class PrePresentCourseCommand : IRequest<PrePresentCourseResponse>
{
    public Guid CourseId { get; set; }
}