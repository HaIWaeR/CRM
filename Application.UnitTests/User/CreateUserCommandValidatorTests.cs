using Application.Behavior.User;
using Application.Validators.User;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.User.Validators
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            CreateUserCommand command = new CreateUserCommand { Name = name };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            CreateUserCommand command = new CreateUserCommand { Name = longName };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            CreateUserCommand command = new CreateUserCommand { Name = "Иван Петров" };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
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
            CreateUserCommand command = new CreateUserCommand { Phone = phone };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            CreateUserCommand command = new CreateUserCommand { Phone = phone };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            CreateUserCommand command = new CreateUserCommand { Phone = "" };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        // Email
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            CreateUserCommand command = new CreateUserCommand { Email = email };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain.")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            CreateUserCommand command = new CreateUserCommand { Email = email };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            CreateUserCommand command = new CreateUserCommand { Email = "" };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateUserCommand command = new CreateUserCommand { Description = longDescription };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateUserCommand command = new CreateUserCommand { Description = "Старший менеджер" };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            CreateUserCommand command = new CreateUserCommand { Description = "" };
            TestValidationResult<CreateUserCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}