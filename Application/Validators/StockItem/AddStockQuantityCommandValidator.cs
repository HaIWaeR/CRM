using Application.Behavior.StockItem;
using FluentValidation;

namespace Application.Validators.StockItem
{
    public class AddStockQuantityCommandValidator : AbstractValidator<AddStockQuantityCommand>
    {
        public AddStockQuantityCommandValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Количество должно быть больше 0");
        }
    }
}