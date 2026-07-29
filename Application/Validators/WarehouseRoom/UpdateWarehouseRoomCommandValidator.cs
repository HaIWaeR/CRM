using Application.Behavior.WarehouseRoom;
using FluentValidation;

namespace Application.Validators.WarehouseRoom
{
    public class UpdateWarehouseRoomCommandValidator : AbstractValidator<UpdateWarehouseRoomCommand>
    {
        public UpdateWarehouseRoomCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название склада обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Адрес склада обязателен")
                .MaximumLength(300).WithMessage("Максимум 300 символов");

            RuleFor(x => x.ContactPerson)
                .MaximumLength(100).WithMessage("Максимум 100 символов")
                .When(x => !string.IsNullOrEmpty(x.ContactPerson));

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\+?[0-9\s\-\(\)]{10,20}$")
                .WithMessage("Некорректный номер телефона")
                .When(x => !string.IsNullOrEmpty(x.ContactPhone));

            RuleFor(x => x.ContactEmail)
                .Must(x => string.IsNullOrEmpty(x) ||
                           (x.Contains("@") &&
                            x.Contains(".") &&
                            x.Split('@')[0].Length > 0 &&
                            x.Split('@')[1].Length > 0 &&
                            x.Split('@')[1].Contains(".") &&
                            x.Split('@')[1].Split('.')[1].Length > 0))
                .WithMessage("Некорректный email")
                .When(x => !string.IsNullOrEmpty(x.ContactEmail));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}