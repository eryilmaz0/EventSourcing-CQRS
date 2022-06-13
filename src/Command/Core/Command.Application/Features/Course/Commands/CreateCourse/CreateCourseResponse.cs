using Command.Application.Abstracts.Command;

namespace Command.Application.Features.Course.Commands.CreateCourse;

public class CreateCourseResponse : CommandResponse
{
    public Guid CourseId { get; set; }
}