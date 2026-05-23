using Application.Behavior.Branch;
using Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.UnitTests.Validators.Branch
{
    public class CreateBranchCommandValidatorTests
    {
        private readonly CreateBranchCommandValidator validator = new();

        [Fact]
        public void CreateBranchCommandValidator_NameIsEmpty_ReturnValidationError()
        {
            CreateBranchCommand command = new CreateBranchCommand { Name = "", Address = "ул. Ленина, 1" };

            TestValidationResult<CreateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBranchCommandValidator_AddressIsEmpty_ReturnValidationError()
        {
            CreateBranchCommand command = new CreateBranchCommand { Name = "Филиал №1", Address = "" };

            TestValidationResult<CreateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("+79998887766")]
        public void CreateBranchCommandValidator_PhoneIsValid_ShouldNotReturnError(string phone)
        {
            CreateBranchCommand command = new CreateBranchCommand { Name = "Филиал", Address = "Адрес", ContactPhone = phone };

            TestValidationResult<CreateBranchCommand> result = validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+79998887766554433")]
        [InlineData("abcdefghij")]
        [InlineData("+7-999-888-77-66")]
        public void CreateBranchCommandValidator_PhoneIsInvalid_ReturnValidationError(string phone)
        {
            CreateBranchCommand command = new CreateBranchCommand { Name = "Филиал", Address = "Адрес", ContactPhone = phone };

            TestValidationResult<CreateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("user.name@domain.co.uk")]
        public void CreateBranchCommandValidator_EmailIsValid_ShouldNotReturnError(string email)
        {
            CreateBranchCommand command = new CreateBranchCommand
            {
                Name = "Филиал",
                Address = "Адрес",
                ContactEmail = email
            };

            TestValidationResult<CreateBranchCommand> result = validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Theory]
        [InlineData("plainaddress")]
        [InlineData("@missingusername.com")]
        [InlineData("username@.com")]
        public void CreateBranchCommandValidator_EmailIsInvalid_ReturnValidationError(string email)
        {
            CreateBranchCommand command = new CreateBranchCommand
            {
                Name = "Филиал",
                Address = "Адрес",
                ContactEmail = email
            };

            TestValidationResult<CreateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        }
    }
}