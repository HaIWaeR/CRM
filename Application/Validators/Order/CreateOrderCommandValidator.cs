using Application.Behavior.Orders;
using FluentValidation;

namespace Application.Validators.Order
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
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

            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("Заказ должен содержать хотя бы один товар")
                .When(x => x.OrderItems != null);
    
            RuleForEach(x => x.OrderItems)
                .Must(x => x.Quantity > 0).WithMessage("Количество должно быть больше 0")
                .When(x => x.OrderItems != null);

            RuleForEach(x => x.OrderItems)
                .Must(x => x.ProductId != Guid.Empty).WithMessage("ID товара обязателен")
                .When(x => x.OrderItems != null);
        }
    }
}