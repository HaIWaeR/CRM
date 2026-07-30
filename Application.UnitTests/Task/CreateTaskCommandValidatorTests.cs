using Application.Behavior.Task;
using Application.Validators.Task;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Task.Validators
{
    public class CreateTaskCommandValidatorTests
    {
        private readonly CreateTaskCommandValidator validator = new();

        // Title
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_TitleIsInvalid_ShouldHaveError(string title)
        {
            CreateTaskCommand command = new CreateTaskCommand { Title = title };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Validate_TitleExceedsMaxLength_ShouldHaveError()
        {
            string longTitle = new string('a', 201);
            CreateTaskCommand command = new CreateTaskCommand { Title = longTitle };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Validate_TitleIsValid_ShouldNotHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { Title = "Установка москитной сетки" };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateTaskCommand command = new CreateTaskCommand { Description = longDescription };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { Description = "Установить сетку на балкон" };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { Description = "" };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // Deadline
        [Fact]
        public void Validate_DeadlineIsPast_ShouldHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { Deadline = DateTime.UtcNow.AddDays(-1) };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Deadline);
        }

        [Fact]
        public void Validate_DeadlineIsFuture_ShouldNotHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { Deadline = DateTime.UtcNow.AddDays(5) };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Deadline);
        }

        [Fact]
        public void Validate_DeadlineIsNull_ShouldNotHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { Deadline = null };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Deadline);
        }

        // AssignedAt
        [Fact]
        public void Validate_AssignedAtIsPast_ShouldHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { AssignedAt = DateTime.UtcNow.AddDays(-1) };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.AssignedAt);
        }

        [Fact]
        public void Validate_AssignedAtIsFuture_ShouldNotHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { AssignedAt = DateTime.UtcNow.AddDays(1) };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.AssignedAt);
        }

        [Fact]
        public void Validate_AssignedAtIsNull_ShouldNotHaveError()
        {
            CreateTaskCommand command = new CreateTaskCommand { AssignedAt = null };
            TestValidationResult<CreateTaskCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.AssignedAt);
        }
    }
}