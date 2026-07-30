using Application.Behavior.Task;
using FluentValidation;

namespace Application.Validators.Task
{
    public class AssignTaskToUserCommandValidator : AbstractValidator<AssignTaskToUserCommand>
    {
        public AssignTaskToUserCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("ID задачи обязателен");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("ID пользователя обязателен");
        }
    }
}