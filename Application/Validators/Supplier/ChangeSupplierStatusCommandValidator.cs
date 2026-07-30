using Application.Behavior.Supplier;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Supplier
{
    public class ChangeSupplierStatusCommandValidator : AbstractValidator<ChangeSupplierStatusCommand>
    {
        public ChangeSupplierStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус поставщика");
        }
    }
}