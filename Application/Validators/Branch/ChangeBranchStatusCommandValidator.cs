using FluentValidation;
using Application.Behavior.Branch;

namespace Application.Validators
{
    public class ChangeBranchStatusCommandValidator : AbstractValidator<ChangeBranchStatusCommand>
    {
        public ChangeBranchStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID филиала обязателен для заполнения.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Указан недопустимый статус филиала.");
        }
    }
}
