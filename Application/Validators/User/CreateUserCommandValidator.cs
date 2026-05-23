using FluentValidation;
using Application.Behavior.User;

namespace Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя пользователя обязательно")
                .MaximumLength(50).WithMessage("Имя не может быть длиннее 50 символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("Неверный формат email");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Пароль обязателен")
                .MinimumLength(6).WithMessage("Пароль должен содержать минимум 6 символов");
        }
    }
}