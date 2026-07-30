using Application.Behavior.Task;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Task
{
    public class ChangeTaskStatusCommandValidator : AbstractValidator<ChangeTaskStatusCommand>
    {
        public ChangeTaskStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Некорректный статус задачи");
        }
    }
}