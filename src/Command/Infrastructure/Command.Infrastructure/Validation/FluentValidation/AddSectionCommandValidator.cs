using Command.Application.Features.Course.Commands.AddSection;
using FluentValidation;

namespace Command.Infrastructure.Validation.FluentValidation;

public class AddSectionCommandValidator : AbstractValidator<AddSectionCommand>
{
    public AddSectionCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId Field Can Not be Null or Empty.");
        RuleFor(x => x.InstructorId).NotEmpty().WithMessage("InstructorId Field Can Not be Null or Empty.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title Field Can Not be Null or Empty.");
        RuleFor(x => x.VideoUrl).NotEmpty().WithMessage("VideoUrl Field Can Not be Null or Empty.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description Field Can Not be Null or Empty.");
    }
}