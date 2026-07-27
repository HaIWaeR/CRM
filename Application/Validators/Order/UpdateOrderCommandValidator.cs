using Application.Behavior.Order;
using Application.Behavior.Orders;
using FluentValidation;

namespace Application.Validators.Order
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.ServiceName)
                .NotEmpty().WithMessage("Название услуги обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Address)
                .MaximumLength(300).WithMessage("Максимум 300 символов")
                .When(x => !string.IsNullOrEmpty(x.Address));
        }
    }
}