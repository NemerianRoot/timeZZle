using FluentValidation;
using timeZZle.Application.Handlers.Clocks;

namespace timeZZle.Application.Validators.Clocks;

internal class CreateClockValidator : AbstractValidator<CreateClockCommand>
{
    public CreateClockValidator()
    {
        RuleFor(o => o.Boxes.Length)
            .Must(o => o > 3)
            .WithMessage("Minimum clock size is 4");

        RuleFor(o => o.Boxes).Must(HaveUniquePosition)
            .WithMessage("Positions must ne unique within a puzzle");
    }

    private static bool HaveUniquePosition(BoxInput[] boxes)
    {
        return boxes.Select(x => x.Position).Distinct().Count() == boxes.Length;
    }
}