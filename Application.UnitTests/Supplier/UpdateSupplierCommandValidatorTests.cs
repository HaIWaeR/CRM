using Application.Behavior.Supplier;
using Application.Validators.Supplier;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Supplier.Validators
{
    public class UpdateSupplierCommandValidatorTests
    {
        private readonly UpdateSupplierCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Name = name };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateSupplierCommand command = new UpdateSupplierCommand { Name = longName };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Name = "ООО СтройМаркет" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Inn
        [Fact]
        public void Validate_InnExceedsMaxLength_ShouldHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Inn = "1234567890123" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Inn);
        }

        [Fact]
        public void Validate_InnWithLetters_ShouldHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Inn = "1234567890A" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Inn);
        }

        [Fact]
        public void Validate_InnIsValid_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Inn = "123456789012" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Inn);
        }

        [Fact]
        public void Validate_InnIsEmpty_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Inn = "" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Inn);
        }

        // Kpp
        [Fact]
        public void Validate_KppExceedsMaxLength_ShouldHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Kpp = "1234567890" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Kpp);
        }

        [Fact]
        public void Validate_KppWithLetters_ShouldHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Kpp = "12345678A" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Kpp);
        }

        [Fact]
        public void Validate_KppIsValid_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Kpp = "123456789" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Kpp);
        }

        [Fact]
        public void Validate_KppIsEmpty_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Kpp = "" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Kpp);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            UpdateSupplierCommand command = new UpdateSupplierCommand { Address = longAddress };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Address = "г. Москва, ул. Ленина, д. 10" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsEmpty_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Address = "" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        // ContactPerson
        [Fact]
        public void Validate_ContactPersonExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            UpdateSupplierCommand command = new UpdateSupplierCommand { ContactPerson = longName };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPerson);
        }

        [Fact]
        public void Validate_ContactPersonIsValid_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { ContactPerson = "Иванов Петр" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ContactPerson);
        }

        // Phone
        [Theory]
        [InlineData("+7 (999) 123-45-67")]
        [InlineData("8-999-123-45-67")]
        [InlineData("+7 999 123 45 67")]
        [InlineData("89991234567")]
        public void Validate_PhoneIsValid_ShouldNotHaveError(string phone)
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Phone = phone };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Phone = phone };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Phone = "" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        // Email
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Email = email };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain.")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Email = email };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Email = "" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        // Website
        [Fact]
        public void Validate_WebsiteExceedsMaxLength_ShouldHaveError()
        {
            string longWebsite = new string('a', 201);
            UpdateSupplierCommand command = new UpdateSupplierCommand { Website = longWebsite };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Website);
        }

        [Fact]
        public void Validate_WebsiteIsValid_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Website = "https://example.com" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Website);
        }

        [Fact]
        public void Validate_WebsiteIsEmpty_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Website = "" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Website);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateSupplierCommand command = new UpdateSupplierCommand { Description = longDescription };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand { Description = "Поставщик строительных материалов" };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // Rating
        [Fact]
        public void Validate_RatingIsLessThanMin_ShouldHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand
            {
                Rating = 0,
                Name = "ООО Тест",
            };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Rating);
        }

        [Fact]
        public void Validate_RatingIsGreaterThanMax_ShouldHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand
            {
                Rating = 11,
                Name = "ООО Тест",
            };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Rating);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void Validate_RatingIsValid_ShouldNotHaveError(int rating)
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand
            {
                Rating = rating,
                Name = "ООО Тест",
            };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Rating);
        }

        [Fact]
        public void Validate_RatingIsNull_ShouldNotHaveError()
        {
            UpdateSupplierCommand command = new UpdateSupplierCommand
            {
                Rating = null,
                Name = "ООО Тест",
            };
            TestValidationResult<UpdateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Rating);
        }
    }
}