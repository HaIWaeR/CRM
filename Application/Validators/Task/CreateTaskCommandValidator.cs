using Application.Behavior.Task;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Task
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название задачи обязательно")
                .MaximumLength(200).WithMessage("Максимум 200 символов");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Максимум 500 символов")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Deadline)
                .GreaterThan(DateTime.UtcNow).WithMessage("Дедлайн должен быть в будущем")
                .When(x => x.Deadline.HasValue);

            RuleFor(x => x.AssignedAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("Дата назначения должна быть в будущем")
                .When(x => x.AssignedAt.HasValue);
        }
    }
}