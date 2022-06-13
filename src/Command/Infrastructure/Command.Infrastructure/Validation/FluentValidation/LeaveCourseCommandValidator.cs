using Command.Application.Features.Course.Commands.LeaveCourse;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class LeaveCourseCommandValidator : AbstractValidator<LeaveCourseCommand>
{
    public LeaveCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
        RuleFor(x => x.ParticipantId).NotEmpty().WithMessage("ParticipantId Field Can Not be Null or Empty.");
    }
}