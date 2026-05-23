using Application.Behavior.Branch;
using Application.Validators;
using FluentValidation.TestHelper;

namespace Application.UnitTests.branch
{
    public class UpdateBranchCommandValidatorTests
    {
        private readonly UpdateBranchCommandValidator validator = new();

        [Fact]
        public void UpdateBranchCommandValidator_NameIsEmpty_ReturnValidationError()
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Name = "", Address = "ул. Ленина, 1" };

            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void UpdateBranchCommandValidator_AddressIsEmpty_ReturnValidationError()
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Name = "Филиал №1", Address = "" };

            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("+79998887766")]
        public void UpdateBranchCommandValidator_PhoneIsValid_ShouldNotReturnError(string phone)
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Name = "Филиал", Address = "Адрес", ContactPhone = phone };

            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+79998887766554433")]
        [InlineData("abcdefghij")]
        [InlineData("+7-999-888-77-66")]
        public void UpdateBranchCommandValidator_PhoneIsInvalid_ReturnValidationError(string phone)
        {
            UpdateBranchCommand command = new UpdateBranchCommand { Name = "Филиал", Address = "Адрес", ContactPhone = phone };

            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("user.name@domain.co.uk")]
        public void UpdateBranchCommandValidator_EmailIsValid_ShouldNotReturnError(string email)
        {
            UpdateBranchCommand command = new UpdateBranchCommand
            {
                Name = "Филиал",
                Address = "Адрес",
                ContactEmail = email
            };

            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Theory]
        [InlineData("plainaddress")]
        [InlineData("@missingusername.com")]
        [InlineData("username@.com")]
        public void UpdateBranchCommandValidator_EmailIsInvalid_ReturnValidationError(string email)
        {
            UpdateBranchCommand command = new UpdateBranchCommand
            {
                Name = "Филиал",
                Address = "Адрес",
                ContactEmail = email
            };

            TestValidationResult<UpdateBranchCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        }
    }
}
