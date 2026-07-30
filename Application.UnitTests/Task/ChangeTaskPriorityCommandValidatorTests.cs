using Application.Behavior.Task;
using Application.Validators.Task;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Task.Validators
{
    public class ChangeTaskPriorityCommandValidatorTests
    {
        private readonly ChangeTaskPriorityCommandValidator validator = new();

        [Theory]
        [InlineData(TaskPriority.Low)]
        [InlineData(TaskPriority.Medium)]
        [InlineData(TaskPriority.High)]
        [InlineData(TaskPriority.Critical)]
        public void Validate_PriorityIsValid_ShouldNotHaveError(TaskPriority priority)
        {
            ChangeTaskPriorityCommand command = new ChangeTaskPriorityCommand
            {
                Id = Guid.NewGuid(),
                Priority = priority
            };

            TestValidationResult<ChangeTaskPriorityCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Priority);
        }

        [Theory]
        [InlineData((TaskPriority)(-1))]
        [InlineData((TaskPriority)0)]
        [InlineData((TaskPriority)99)]
        public void Validate_PriorityIsInvalid_ShouldHaveError(TaskPriority priority)
        {
            ChangeTaskPriorityCommand command = new ChangeTaskPriorityCommand
            {
                Id = Guid.NewGuid(),
                Priority = priority
            };

            TestValidationResult<ChangeTaskPriorityCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Priority);
        }
    }
}