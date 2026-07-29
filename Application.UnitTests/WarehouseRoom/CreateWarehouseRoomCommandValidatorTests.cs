using Application.Behavior.WarehouseRoom;
using Application.Validators.WarehouseRoom;
using FluentValidation.TestHelper;

namespace Application.UnitTests.WarehouseRoom.Validators
{
    public class CreateWarehouseRoomCommandValidatorTests
    {
        private readonly CreateWarehouseRoomCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Name = name };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Name = longName };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Name = "Склад материалов" };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Address
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_AddressIsInvalid_ShouldHaveError(string address)
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Address = address };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Address = longAddress };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Address = "г. Москва, ул. Производственная, д. 8" };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        // ContactPerson
        [Fact]
        public void Validate_ContactPersonExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactPerson = longName };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPerson);
        }

        [Fact]
        public void Validate_ContactPersonIsValid_ShouldNotHaveError()
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactPerson = "Иванов Иван" };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
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
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactPhone = phone };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactPhone = phone };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPhone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactPhone = "" };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPhone);
        }

        // ContactEmail
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactEmail = email };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain.")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactEmail = email };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { ContactEmail = "" };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Description = longDescription };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateWarehouseRoomCommand command = new CreateWarehouseRoomCommand { Description = "Основной склад для хранения материалов" };
            TestValidationResult<CreateWarehouseRoomCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}