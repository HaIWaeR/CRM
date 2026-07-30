using Application.Behavior.Product;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Product
{
    public class ChangeProductStatusCommandValidator : AbstractValidator<ChangeProductStatusCommand>
    {
        public ChangeProductStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус продукта");
        }
    }
}