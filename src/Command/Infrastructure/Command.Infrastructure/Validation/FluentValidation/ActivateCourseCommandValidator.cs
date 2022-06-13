using Command.Application.Features.Course.Commands.ActivateCourse;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class ActivateCourseCommandValidator : AbstractValidator<ActivateCourseCommand>
{
    public ActivateCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
    }
}