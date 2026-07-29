using Application.Behavior.User;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя пользователя обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Некорректная роль пользователя");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9\s\-\(\)]{10,20}$")
                .WithMessage("Некорректный номер телефона")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Email)
                .Must(x => string.IsNullOrEmpty(x) ||
                           (x.Contains("@") &&
                            x.Contains(".") &&
                            x.Split('@')[0].Length > 0 &&
                            x.Split('@')[1].Length > 0 &&
                            x.Split('@')[1].Contains(".") &&
                            x.Split('@')[1].Split('.')[1].Length > 0))
                .WithMessage("Некорректный Email")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}