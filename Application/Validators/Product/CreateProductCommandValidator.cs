using Application.Behavior.Product;
using FluentValidation;

namespace Application.Validators.Product
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название товара обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Цена не может быть отрицательной");

            RuleFor(x => x.Category)
                .MaximumLength(50).WithMessage("Максимум 50 символов")
                .When(x => !string.IsNullOrEmpty(x.Category));

            RuleFor(x => x.Article)
                .MaximumLength(50).WithMessage("Максимум 50 символов")
                .Matches(@"^[A-Za-z0-9\-_]+$").WithMessage("Только буквы, цифры, - и _")
                .When(x => !string.IsNullOrEmpty(x.Article));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}