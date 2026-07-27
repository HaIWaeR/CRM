using Application.Behavior.StockItem;
using FluentValidation;

namespace Application.Validators.StockItem
{
    public class RemoveStockQuantityCommandValidator : AbstractValidator<RemoveStockQuantityCommand>
    {
        public RemoveStockQuantityCommandValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Количество должно быть больше 0");
        }
    }
}