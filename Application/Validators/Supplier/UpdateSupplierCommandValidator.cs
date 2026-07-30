using Application.Behavior.Supplier;
using FluentValidation;

namespace Application.Validators.Supplier
{
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название поставщика обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Inn)
                .MaximumLength(12).WithMessage("Максимум 12 символов")
                .Matches(@"^[0-9]+$").WithMessage("ИНН должен содержать только цифры")
                .When(x => !string.IsNullOrEmpty(x.Inn));

            RuleFor(x => x.Kpp)
                .MaximumLength(9).WithMessage("Максимум 9 символов")
                .Matches(@"^[0-9]+$").WithMessage("КПП должен содержать только цифры")
                .When(x => !string.IsNullOrEmpty(x.Kpp));

            RuleFor(x => x.Address)
                .MaximumLength(300).WithMessage("Максимум 300 символов")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.ContactPerson)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.ContactPerson));

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

            RuleFor(x => x.Website)
                .MaximumLength(200).WithMessage("Максимум 200 символов")
                .When(x => !string.IsNullOrEmpty(x.Website));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 10).WithMessage("Рейтинг должен быть от 1 до 10")
                .When(x => x.Rating.HasValue);
        }
    }
}