using Command.Application.Features.Course.Commands.ChangeCourseTitle;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class ChangeCourseTitleCommandValidator : AbstractValidator<ChangeCourseTitleCommand>
{
    public ChangeCourseTitleCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title Field Can Not be Null or Empty.");
    }
}