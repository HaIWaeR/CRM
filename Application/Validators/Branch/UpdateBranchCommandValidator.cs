using FluentValidation;
using Application.Behavior.Branch;

namespace Application.Validators
{
    public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название филиала обязательно");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Адрес филиала обязателен");

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\+?\d{10,15}$").When(x => !string.IsNullOrWhiteSpace(x.ContactPhone))
                .WithMessage("Неверный формат телефона");

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("Email обязателен")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("Неверный формат email");
        }
    }
}