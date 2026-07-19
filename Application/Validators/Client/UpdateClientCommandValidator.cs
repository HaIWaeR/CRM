
using Application.Behavior.Client;
using FluentValidation;

namespace Application.Validators.Client
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100).WithMessage("Максимум 100 символов");

            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.MiddleName)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.MiddleName));

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

            RuleFor(x => x.Telegram)
                .Must(x => string.IsNullOrEmpty(x) || x.StartsWith("@"))
                .WithMessage("Telegram должен начинаться с @")
                .MaximumLength(50).WithMessage("Максимум 50 символов")
                .When(x => !string.IsNullOrEmpty(x.Telegram));

            RuleFor(x => x.Address)
                .MaximumLength(300).WithMessage("Максимум 300 символов")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Notes));

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow).WithMessage("Дата рождения не может быть в будущем")
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.Phone) ||
                           !string.IsNullOrWhiteSpace(x.Email) ||
                           !string.IsNullOrWhiteSpace(x.Telegram))
                .WithMessage("Укажите хотя бы один контакт (телефон, Email или Telegram)");
        }
    }
}
