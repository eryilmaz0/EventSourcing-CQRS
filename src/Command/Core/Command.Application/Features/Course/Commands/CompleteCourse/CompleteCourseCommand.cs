using MediatR;

namespace Command.Application.Features.Course.Commands.CompleteCourse;

public class CompleteCourseCommand : IRequest<CompleteCourseResponse>
{
    public Guid CourseId { get; set; }
}