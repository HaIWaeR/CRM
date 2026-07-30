using Application.Behavior.Material;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Material
{
    public class ChangeMaterialStatusCommandValidator : AbstractValidator<ChangeMaterialStatusCommand>
    {
        public ChangeMaterialStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус материала");
        }
    }
}