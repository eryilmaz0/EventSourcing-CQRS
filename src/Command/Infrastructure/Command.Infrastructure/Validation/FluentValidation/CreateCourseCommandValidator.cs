using Command.Application.Features.Course.Commands.CreateCourse;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.InstructorId).NotEmpty().WithMessage("InstructorId Field Can Not be Null or Empty.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title Field Can Not be Null or Empty.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description Field Can Not be Null or Empty.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Field Can Not be Null or Empty.");
    }
}