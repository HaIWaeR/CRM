using Application.Behavior.Task;
using Application.Validators.Task;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Task.Validators
{
    public class UpdateTaskCommandValidatorTests
    {
        private readonly UpdateTaskCommandValidator validator = new();

        // Title
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_TitleIsInvalid_ShouldHaveError(string title)
        {
            UpdateTaskCommand command = new UpdateTaskCommand { Title = title };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Validate_TitleExceedsMaxLength_ShouldHaveError()
        {
            string longTitle = new string('a', 201);
            UpdateTaskCommand command = new UpdateTaskCommand { Title = longTitle };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Validate_TitleIsValid_ShouldNotHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { Title = "Установка москитной сетки" };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateTaskCommand command = new UpdateTaskCommand { Description = longDescription };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { Description = "Установить сетку на балкон" };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { Description = "" };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // Deadline
        [Fact]
        public void Validate_DeadlineIsPast_ShouldHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { Deadline = DateTime.UtcNow.AddDays(-1) };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Deadline);
        }

        [Fact]
        public void Validate_DeadlineIsFuture_ShouldNotHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { Deadline = DateTime.UtcNow.AddDays(5) };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Deadline);
        }

        [Fact]
        public void Validate_DeadlineIsNull_ShouldNotHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { Deadline = null };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Deadline);
        }

        // AssignedAt
        [Fact]
        public void Validate_AssignedAtIsPast_ShouldHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { AssignedAt = DateTime.UtcNow.AddDays(-1) };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.AssignedAt);
        }

        [Fact]
        public void Validate_AssignedAtIsFuture_ShouldNotHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { AssignedAt = DateTime.UtcNow.AddDays(1) };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.AssignedAt);
        }

        [Fact]
        public void Validate_AssignedAtIsNull_ShouldNotHaveError()
        {
            UpdateTaskCommand command = new UpdateTaskCommand { AssignedAt = null };
            TestValidationResult<UpdateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.AssignedAt);
        }
    }
}