using FluentValidation;
using Application.Behavior.Material;

namespace Application.Validators.Material
{
    public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
    {
        public CreateMaterialCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название материала обязательно.")
            .MaximumLength(100).WithMessage("Название слишком длинное.");

            RuleFor(x => x.CategoryCode)
                .NotEmpty().WithMessage("Категория обязательна.")
                .Matches("^[A-Z]{3}-[A-Z]{3}$").WithMessage("Неверный формат категории. Пример: MET-TIT");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Количество не может быть отрицательным.");

            RuleFor(x => x.PriceUnit)
                .GreaterThan(0).WithMessage("Цена должна быть больше нуля.");
        }
    }
}