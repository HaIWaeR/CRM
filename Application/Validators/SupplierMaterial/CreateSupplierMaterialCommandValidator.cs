using Application.Behavior.SupplierMaterial;
using FluentValidation;

namespace Application.Validators.SupplierMaterial
{
    public class CreateSupplierMaterialCommandValidator : AbstractValidator<CreateSupplierMaterialCommand>
    {
        public CreateSupplierMaterialCommandValidator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage("ID поставщика обязателен");

            RuleFor(x => x.MaterialId)
                .NotEmpty().WithMessage("ID материала обязателен");

            RuleFor(x => x.PriceUnit)
                .GreaterThanOrEqualTo(0).WithMessage("Цена не может быть отрицательной")
                .When(x => x.PriceUnit.HasValue);

            RuleFor(x => x.DeliveryDays)
                .GreaterThan(0).WithMessage("Срок поставки должен быть больше 0")
                .When(x => x.DeliveryDays.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}