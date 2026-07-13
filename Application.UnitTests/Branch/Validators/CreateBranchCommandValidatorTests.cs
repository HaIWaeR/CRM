using Application.Behavior.Branch;
using Application.Validators;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Branch.Validators
{
    public class CreateBranchCommandValidatorTests
    {
        private readonly CreateBranchCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            var command = new CreateBranchCommand { Name = name };
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            var command = new CreateBranchCommand { Name = "Главный офис" };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]  
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            var command = new CreateBranchCommand { Name = longName };
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            var command = new CreateBranchCommand { Address = longAddress };
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            var command = new CreateBranchCommand { Address = "ул. Ленина, 1" };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsEmpty_ShouldNotHaveError()
        {
            var command = new CreateBranchCommand { Address = "" };
            var result = validator.TestValidate(command);
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
            var command = new CreateBranchCommand { ContactPhone = phone };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            var command = new CreateBranchCommand { ContactPhone = phone };
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            var command = new CreateBranchCommand { ContactPhone = "" };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        // Email
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            var command = new CreateBranchCommand { ContactEmail = email };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            var command = new CreateBranchCommand { ContactEmail = email };
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            var command = new CreateBranchCommand { ContactEmail = "" };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }
    }
}