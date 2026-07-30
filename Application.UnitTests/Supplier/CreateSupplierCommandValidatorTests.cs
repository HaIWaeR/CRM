using Application.Behavior.Supplier;
using Application.Validators.Supplier;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Supplier.Validators
{
    public class CreateSupplierCommandValidatorTests
    {
        private readonly CreateSupplierCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Name = name };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            CreateSupplierCommand command = new CreateSupplierCommand { Name = longName };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Name = "ООО СтройМаркет" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Inn
        [Fact]
        public void Validate_InnExceedsMaxLength_ShouldHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Inn = "1234567890123" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Inn);
        }

        [Fact]
        public void Validate_InnWithLetters_ShouldHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Inn = "1234567890A" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Inn);
        }

        [Fact]
        public void Validate_InnIsValid_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Inn = "123456789012" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Inn);
        }

        [Fact]
        public void Validate_InnIsEmpty_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Inn = "" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Inn);
        }

        // Kpp
        [Fact]
        public void Validate_KppExceedsMaxLength_ShouldHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Kpp = "1234567890" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Kpp);
        }

        [Fact]
        public void Validate_KppWithLetters_ShouldHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Kpp = "12345678A" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Kpp);
        }

        [Fact]
        public void Validate_KppIsValid_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Kpp = "123456789" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Kpp);
        }

        [Fact]
        public void Validate_KppIsEmpty_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Kpp = "" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Kpp);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            CreateSupplierCommand command = new CreateSupplierCommand { Address = longAddress };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Address = "г. Москва, ул. Ленина, д. 10" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsEmpty_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Address = "" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        // ContactPerson
        [Fact]
        public void Validate_ContactPersonExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            CreateSupplierCommand command = new CreateSupplierCommand { ContactPerson = longName };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ContactPerson);
        }

        [Fact]
        public void Validate_ContactPersonIsValid_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { ContactPerson = "Иванов Петр" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
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
            CreateSupplierCommand command = new CreateSupplierCommand { Phone = phone };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("+123")]
        [InlineData("abcdef")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Phone = phone };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Fact]
        public void Validate_PhoneIsEmpty_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Phone = "" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        // Email
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Email = email };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain.")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Email = email };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validate_EmailIsEmpty_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Email = "" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        // Website
        [Fact]
        public void Validate_WebsiteExceedsMaxLength_ShouldHaveError()
        {
            string longWebsite = new string('a', 201);
            CreateSupplierCommand command = new CreateSupplierCommand { Website = longWebsite };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Website);
        }

        [Fact]
        public void Validate_WebsiteIsValid_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Website = "https://example.com" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Website);
        }

        [Fact]
        public void Validate_WebsiteIsEmpty_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Website = "" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Website);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateSupplierCommand command = new CreateSupplierCommand { Description = longDescription };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand { Description = "Поставщик строительных материалов" };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // Rating
        [Fact]
        public void Validate_RatingIsLessThanMin_ShouldHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand
            {
                Rating = 0,
                Name = "ООО Тест",
            };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Rating);
        }

        [Fact]
        public void Validate_RatingIsGreaterThanMax_ShouldHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand
            {
                Rating = 11,
                Name = "ООО Тест",
            };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Rating);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void Validate_RatingIsValid_ShouldNotHaveError(int rating)
        {
            CreateSupplierCommand command = new CreateSupplierCommand
            {
                Rating = rating,
                Name = "ООО Тест",
            };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Rating);
        }

        [Fact]
        public void Validate_RatingIsNull_ShouldNotHaveError()
        {
            CreateSupplierCommand command = new CreateSupplierCommand
            {
                Rating = null,
                Name = "ООО Тест",
            };
            TestValidationResult<CreateSupplierCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Rating);
        }
    }
}