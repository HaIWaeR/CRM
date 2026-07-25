using FluentValidation;
using Application.Behavior.Material;

namespace Application.Validators.Material
{
    public class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
    {
        public UpdateMaterialCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название материала обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Article)
                .MaximumLength(50).WithMessage("Максимум 50 символов")
                .Matches(@"^[A-Za-z0-9\-_]+$").WithMessage("Только буквы, цифры, - и _")
                .When(x => !string.IsNullOrEmpty(x.Article));

            RuleFor(x => x.CategoryCode)
                .MaximumLength(20).WithMessage("Максимум 20 символов")
                .When(x => !string.IsNullOrEmpty(x.CategoryCode));

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Количество не может быть отрицательным");

            RuleFor(x => x.PriceUnit)
                .GreaterThanOrEqualTo(0).WithMessage("Цена не может быть отрицательной");

            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(0).WithMessage("Вес не может быть отрицательным");

            RuleFor(x => x.UnitMeasurement)
                .IsInEnum().WithMessage("Некорректная единица измерения");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.AdditionInformation)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.AdditionInformation));
        }
    }
}