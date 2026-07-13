using FluentValidation;
using Application.Behavior.Branch;

namespace Application.Validators
{
    public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название филиала обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Address)
                .MaximumLength(300).WithMessage("Максимум 300 символов")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\+?[0-9\s\-\(\)]{10,20}$")
                .WithMessage("Некорректный номер телефона")
                .When(x => !string.IsNullOrEmpty(x.ContactPhone));

            RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Некорректный email")
                .When(x => !string.IsNullOrEmpty(x.ContactEmail));
        }
    }
}