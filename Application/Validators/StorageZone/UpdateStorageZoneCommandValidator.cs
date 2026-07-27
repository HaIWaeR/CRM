using Application.Behavior.StorageZone;
using FluentValidation;

namespace Application.Validators.StorageZone
{
    public class UpdateStorageZoneCommandValidator : AbstractValidator<UpdateStorageZoneCommand>
    {
        public UpdateStorageZoneCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название зоны обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("Максимум 50 символов")
                .Matches(@"^[A-Za-z0-9\-_]+$").WithMessage("Только буквы, цифры, - и _")
                .When(x => !string.IsNullOrEmpty(x.Code));

            RuleFor(x => x.WarehouseId)
                .NotEmpty().WithMessage("ID склада обязателен");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.MaxCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("Максимальная вместимость не может быть отрицательной")
                .When(x => x.MaxCapacity.HasValue);
        }
    }
}