using Application.Behavior.StorageZone;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.StorageZone
{
    public class ChangeStorageZoneStatusCommandValidator : AbstractValidator<ChangeStorageZoneStatusCommand>
    {
        public ChangeStorageZoneStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус зоны хранения");
        }
    }
}