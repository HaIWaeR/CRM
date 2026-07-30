using Application.Behavior.Task;
using Application.Validators.Task;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Task.Validators
{
    public class ChangeTaskStatusCommandValidatorTests
    {
        private readonly ChangeTaskStatusCommandValidator validator = new();

        [Theory]
        [InlineData(InstallTaskStatus.New)]
        [InlineData(InstallTaskStatus.InProgress)]
        [InlineData(InstallTaskStatus.Pending)]
        [InlineData(InstallTaskStatus.Completed)]
        [InlineData(InstallTaskStatus.Cancelled)]
        public void Validate_StatusIsValid_ShouldNotHaveError(InstallTaskStatus status)
        {
            ChangeTaskStatusCommand command = new ChangeTaskStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeTaskStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((InstallTaskStatus)(-1))]
        [InlineData((InstallTaskStatus)0)]
        [InlineData((InstallTaskStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(InstallTaskStatus status)
        {
            ChangeTaskStatusCommand command = new ChangeTaskStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeTaskStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}