using Application.Behavior.User;
using FluentValidation;

namespace Application.Validators.User
{
    public class ChangeUserStatusCommandValidator : AbstractValidator<ChangeUserStatusCommand>
    {
        public ChangeUserStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус пользователя");
        }
    }
}