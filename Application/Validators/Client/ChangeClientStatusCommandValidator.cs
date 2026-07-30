using FluentValidation;
using Application.Behavior.Client;

namespace Application.Validators
{
    public class ChangeClientStatusCommandValidator : AbstractValidator<ChangeClientStatusCommand>
    {
        public ChangeClientStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Указан недопустимый статус филиала.");
        }
    }
}
