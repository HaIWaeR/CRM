using Application.Behavior.Supplier;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Supplier
{
    public class ChangeSupplierTypeCommandValidator : AbstractValidator<ChangeSupplierTypeCommand>
    {
        public ChangeSupplierTypeCommandValidator()
        {
            RuleFor(x => x.SupplierType)
                .IsInEnum().WithMessage("Некорректный тип поставщика");
        }
    }
}