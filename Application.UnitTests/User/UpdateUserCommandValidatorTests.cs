using Application.Behavior.User;
using Application.Validators.User;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.User.Validators
{
    public class UpdateUserCommandValidatorTests
    {
        private readonly UpdateUserCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            UpdateUserCommand command = new UpdateUserCommand { Name = name };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateUserCommand command = new UpdateUserCommand { Name = longName };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            UpdateUserCommand command = new UpdateUserCommand { Name = "Иван Петров" };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Phone
        [Theory]
        [InlineData("+7 (999) 123-45-67")]
        [InlineData("8-999-123-45-67")]
        [InlineData("+7 999 123 45 67")]
        [InlineData("89991234567")]
        public void Validate_PhoneIsValid_ShouldNotHaveError(string phone)
        {
            UpdateUserCommand command = new UpdateUserCommand { Phone = phone };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            UpdateUserCommand command = new UpdateUserCommand { Phone = phone };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            UpdateUserCommand command = new UpdateUserCommand { Phone = "" };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        // Email
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            UpdateUserCommand command = new UpdateUserCommand { Email = email };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain.")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            UpdateUserCommand command = new UpdateUserCommand { Email = email };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            UpdateUserCommand command = new UpdateUserCommand { Email = "" };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateUserCommand command = new UpdateUserCommand { Description = longDescription };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateUserCommand command = new UpdateUserCommand { Description = "Старший менеджер" };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            UpdateUserCommand command = new UpdateUserCommand { Description = "" };
            TestValidationResult<UpdateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}