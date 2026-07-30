using Application.Behavior.Task;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Task
{
    public class ChangeTaskPriorityCommandValidator : AbstractValidator<ChangeTaskPriorityCommand>
    {
        public ChangeTaskPriorityCommandValidator()
        {
            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Некорректный приоритет задачи");
        }
    }
}