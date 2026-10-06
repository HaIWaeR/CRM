using FluentValidation;
using Application.Behavior.Client;

namespace Application.Validators
{
    public class ChangeClientStatusCommandValidator : AbstractValidator<ChangeClientStatusCommand>
    {
        /// <summary>
        /// Задаёт правило: статус должен быть одним из значений <see cref="ClientStatus"/>.
        /// </summary>
        public ChangeClientStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Указан недопустимый статус филиала.");
        }
    }
}
