using Application.Behavior.Task;
using Application.Validators.Task;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Task.Validators
{
    public class AssignTaskToUserCommandValidatorTests
    {
        private readonly AssignTaskToUserCommandValidator validator = new();

        [Fact]
        public void Validate_TaskIdIsEmpty_ShouldHaveError()
        {
            AssignTaskToUserCommand command = new AssignTaskToUserCommand
            {
                TaskId = Guid.Empty,
                UserId = Guid.NewGuid()
            };

            TestValidationResult<AssignTaskToUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.TaskId);
        }

        [Fact]
        public void Validate_UserIdIsEmpty_ShouldHaveError()
        {
            AssignTaskToUserCommand command = new AssignTaskToUserCommand
            {
                TaskId = Guid.NewGuid(),
                UserId = Guid.Empty
            };

            TestValidationResult<AssignTaskToUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Validate_Valid_ShouldNotHaveError()
        {
            AssignTaskToUserCommand command = new AssignTaskToUserCommand
            {
                TaskId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };

            TestValidationResult<AssignTaskToUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.TaskId);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
        }
    }
}