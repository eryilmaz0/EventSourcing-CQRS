using Command.Application.Features.Course.Commands.CommentCourse;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class CommentCourseCommandValidator : AbstractValidator<CommentCourseCommand>
{
    public CommentCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
        RuleFor(x => x.CommentorId).NotEmpty().WithMessage("CommentorId Field Can Not be Null or Empty.");
        RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment Field Can Not be Null or Empty.");
    }
}