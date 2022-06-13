using MediatR;

namespace Command.Application.Features.Course.Commands.CompleteCourse;

public class CompleteCourseCommand : Abstracts.Command.Command, IRequest<CompleteCourseResponse>
{
    public Guid CourseId { get; set; }
}