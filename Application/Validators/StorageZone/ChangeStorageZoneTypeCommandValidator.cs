using Application.Behavior.StorageZone;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.StorageZone
{
    public class ChangeStorageZoneTypeCommandValidator : AbstractValidator<ChangeStorageZoneTypeCommand>
    {
        public ChangeStorageZoneTypeCommandValidator()
        {
            RuleFor(x => x.ZoneType)
                .IsInEnum().WithMessage("Некорректный тип зоны хранения");
        }
    }
}