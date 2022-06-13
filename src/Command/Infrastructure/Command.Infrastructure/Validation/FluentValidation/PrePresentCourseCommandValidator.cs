using Command.Application.Features.Course.Commands.PrePresentCourse;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class PrePresentCourseCommandValidator : AbstractValidator<PrePresentCourseCommand>
{
    public PrePresentCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
    }
}