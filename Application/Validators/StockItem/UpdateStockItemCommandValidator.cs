using Application.Behavior.StockItem;
using FluentValidation;

namespace Application.Validators.StockItem
{
    public class UpdateStockItemCommandValidator : AbstractValidator<UpdateStockItemCommand>
    {
        public UpdateStockItemCommandValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Количество должно быть больше 0");

            RuleFor(x => x.WarehouseId)
                .NotEmpty().WithMessage("ID склада обязателен");

            RuleFor(x => x)
                .Must(x => x.ProductId.HasValue || x.MaterialId.HasValue)
                .WithMessage("Должен быть указан либо ProductId, либо MaterialId");

            RuleFor(x => x)
                .Must(x => !(x.ProductId.HasValue && x.MaterialId.HasValue))
                .WithMessage("Нельзя указывать одновременно и ProductId, и MaterialId");
        }
    }
}