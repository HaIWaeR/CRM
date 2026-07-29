using Application.Behavior.WarehouseRoom;
using Application.Validators.WarehouseRoom;
using FluentValidation.TestHelper;

namespace Application.UnitTests.WarehouseRoom.Validators
{
    public class UpdateWarehouseRoomCommandValidatorTests
    {
        private readonly UpdateWarehouseRoomCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Name = name };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Name = longName };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Name = "Склад материалов" };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Address
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_AddressIsInvalid_ShouldHaveError(string address)
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Address = address };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Address = longAddress };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Address = "г. Москва, ул. Производственная, д. 8" };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        // ContactPerson
        [Fact]
        public void Validate_ContactPersonExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactPerson = longName };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPerson);
        }

        [Fact]
        public void Validate_ContactPersonIsValid_ShouldNotHaveError()
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactPerson = "Иванов Иван" };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPerson);
        }

        // ContactPhone
        [Theory]
        [InlineData("+7 (999) 123-45-67")]
        [InlineData("8-999-123-45-67")]
        [InlineData("+7 999 123 45 67")]
        [InlineData("89991234567")]
        public void Validate_PhoneIsValid_ShouldNotHaveError(string phone)
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactPhone = phone };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactPhone = phone };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactPhone = "" };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        // ContactEmail
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactEmail = email };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain.")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactEmail = email };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { ContactEmail = "" };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Description = longDescription };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateWarehouseRoomCommand command = new UpdateWarehouseRoomCommand { Description = "Основной склад для хранения материалов" };
            TestValidationResult<UpdateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}