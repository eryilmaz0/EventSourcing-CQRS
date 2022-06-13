using Command.Application.Features.Course.Commands.JoinCourse;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class JoinCourseCommandValidator : AbstractValidator<JoinCourseCommand>
{
    public JoinCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
        RuleFor(x => x.ParticipantId).NotEmpty().WithMessage("ParticipantId Field Can Not be Null or Empty.");
    }
}