using Application.Behavior.Branch;
using Application.Validators;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Branch.Validators
{
    public class UpdateBranchCommandValidatorTests
    {
        private readonly UpdateBranchCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Name = name };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Name = "Главный офис" };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateBranchCommand command = new UpdateBranchCommand { Name = longName };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            UpdateBranchCommand command = new UpdateBranchCommand { Address = longAddress };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Address = "ул. Ленина, 1" };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsEmpty_ShouldNotHaveError()
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Address = "" };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        // Phone
        [Theory]
        [InlineData("+7 (999) 123-45-67")]
        [InlineData("8-999-123-45-67")]
        [InlineData("+7 999 123 45 67")]
        [InlineData("89991234567")]
        public void Validate_PhoneIsValid_ShouldNotHaveError(string phone)
        {
            UpdateBranchCommand command = new UpdateBranchCommand { ContactPhone = phone };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            UpdateBranchCommand command = new UpdateBranchCommand { ContactPhone = phone };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            UpdateBranchCommand command = new UpdateBranchCommand { ContactPhone = "" };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        // Email
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            UpdateBranchCommand command = new UpdateBranchCommand { ContactEmail = email };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            UpdateBranchCommand command = new UpdateBranchCommand { ContactEmail = email };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            UpdateBranchCommand command = new UpdateBranchCommand { ContactEmail = "" };
            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }
    }
}