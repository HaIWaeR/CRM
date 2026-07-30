using Application.Behavior.WarehouseRoom;
using FluentValidation;

namespace Application.Validators.WarehouseRoom
{
    public class ChangeWarehouseStatusCommandValidator : AbstractValidator<ChangeWarehouseStatusCommand>
    {
        public ChangeWarehouseStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус склада");
        }
    }
}