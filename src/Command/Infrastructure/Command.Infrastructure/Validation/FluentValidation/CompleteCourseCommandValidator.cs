using Command.Application.Features.Course.Commands.CompleteCourse;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class CompleteCourseCommandValidator : AbstractValidator<CompleteCourseCommand>
{
    public CompleteCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
    }
}