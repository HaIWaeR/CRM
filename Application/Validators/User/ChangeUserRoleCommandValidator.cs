using Application.Behavior.User;
using FluentValidation;

namespace Application.Validators.User
{
    public class ChangeUserRoleCommandValidator : AbstractValidator<ChangeUserRoleCommand>
    {
        public ChangeUserRoleCommandValidator()
        {
            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Некорректная роль пользователя");
        }
    }
}