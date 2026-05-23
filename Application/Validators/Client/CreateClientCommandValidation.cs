using Application.Behavior.Branch;
using Application.Behavior.Client;
using FluentValidation;

namespace Application.Validators.Client
{
    public class CreateClientCommandValidation : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidation()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Название филиала обязательно")
                 .MaximumLength(100).WithMessage("Название не может быть длиннее 100 символов");

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.Email) || !string.IsNullOrWhiteSpace(x.Phone) || !string.IsNullOrWhiteSpace(x.Telegram))
                .WithMessage("Должен быть указан хотя бы один контакт: email, телефон или телеграмм");

            RuleFor(x => x.Email)
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("Неверный формат email")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Phone)
                .Matches(@"^\+?\d{10,15}$").When(x => !string.IsNullOrWhiteSpace(x.Phone))
                .WithMessage("Неверный формат телефона");

            RuleFor(x => x.Telegram)
                .Matches(@"^@[\w]{5,32}$").WithMessage("Telegram должен начинаться с '@' и содержать от 5 до 32 символов")
                .When(x => !string.IsNullOrWhiteSpace(x.Telegram));
        }
    }
}
