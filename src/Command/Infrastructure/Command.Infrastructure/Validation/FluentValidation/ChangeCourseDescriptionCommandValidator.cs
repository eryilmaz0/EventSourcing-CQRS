using Command.Application.Features.Course.Commands.ChangeCourseDescription;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class ChangeCourseDescriptionCommandValidator : AbstractValidator<ChangeCourseDescriptionCommand>
{
    public ChangeCourseDescriptionCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description Field Can Not be Null or Empty.");
    }
}