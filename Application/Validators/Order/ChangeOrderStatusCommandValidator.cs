using Application.Behavior.Order;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Order
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус заказа");
        }
    }
}