using FluentValidation;
using timeZZle.Application.Handlers;

namespace timeZZle.Application.Validators;

public class CreateClockValidator : AbstractValidator<CreateClockCommand>
{
    public CreateClockValidator()
    {
        RuleFor(o => o.Boxes.Length).Must(o => o > 4);
    }
}