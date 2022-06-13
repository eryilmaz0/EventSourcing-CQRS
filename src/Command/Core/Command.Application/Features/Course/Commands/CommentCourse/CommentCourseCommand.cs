using MediatR;

namespace Command.Application.Features.Course.Commands.CommentCourse;

public class CommentCourseCommand : Abstracts.Command.Command, IRequest<CommentCourseResponse>
{
    public Guid CourseId { get; set; }
    public Guid CommentorId { get; set; }
    public string Comment { get; set; }
}