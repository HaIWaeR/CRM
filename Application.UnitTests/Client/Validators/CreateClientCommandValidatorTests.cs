using Application.Behavior.Client;
using Application.Validators.Client;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Client.Validators
{
    public class CreateClientCommandValidatorTests
    {
        private readonly CreateClientCommandValidator validator = new();

        // FirstName
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_FirstNameIsInvalid_ShouldHaveError(string firstName)
        {
            CreateClientCommand command = new CreateClientCommand { FirstName = firstName };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Validate_FirstNameIsValid_ShouldNotHaveError()
        {
            CreateClientCommand command = new CreateClientCommand { FirstName = "Виталий" };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Validate_FirstNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            CreateClientCommand command = new CreateClientCommand { FirstName = longName };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        // LastName
        [Fact]
        public void Validate_LastNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            CreateClientCommand command = new CreateClientCommand { LastName = longName };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Fact]
        public void Validate_LastNameIsValid_ShouldNotHaveError()
        {
            CreateClientCommand command = new CreateClientCommand { LastName = "Папков" };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        }

        // MiddleName
        [Fact]
        public void Validate_MiddleNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            CreateClientCommand command = new CreateClientCommand { MiddleName = longName };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.MiddleName);
        }

        [Fact]
        public void Validate_MiddleNameIsValid_ShouldNotHaveError()
        {
            CreateClientCommand command = new CreateClientCommand { MiddleName = "Алексеевич" };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MiddleName);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            CreateClientCommand command = new CreateClientCommand { Address = longAddress };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            CreateClientCommand command = new CreateClientCommand { Address = "ул. Ленина, 1" };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        // Notes
        [Fact]
        public void Validate_NotesExceedsMaxLength_ShouldHaveError()
        {
            string longNotes = new string('a', 501);
            CreateClientCommand command = new CreateClientCommand { Notes = longNotes };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void Validate_NotesIsValid_ShouldNotHaveError()
        {
            CreateClientCommand command = new CreateClientCommand { Notes = "Постоянный клиент" };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Notes);
        }

        // Telegram
        [Fact]
        public void Validate_TelegramWithoutAtSymbol_ShouldHaveError()
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67",
                Telegram = "haiwaer"
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Telegram);
        }

        [Fact]
        public void Validate_TelegramWithAtSymbol_ShouldNotHaveError()
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67",
                Telegram = "@haiwaer"
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Telegram);
        }

        [Fact]
        public void Validate_TelegramExceedsMaxLength_ShouldHaveError()
        {
            string longTelegram = new string('a', 51);
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67",
                Telegram = "@" + longTelegram
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Telegram);
        }

        // Phone
        [Theory]
        [InlineData("+7 (999) 123-45-67")]
        [InlineData("8-999-123-45-67")]
        [InlineData("+7 999 123 45 67")]
        [InlineData("89991234567")]
        public void Validate_PhoneIsValid_ShouldNotHaveError(string phone)
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = phone
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("abcdef")]
        [InlineData("+7(999)")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = phone
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        // Email
        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test")]
        [InlineData("test@domain")]
        [InlineData("test@domain.")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67",
                Email = email
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67",
                Email = email
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        // BirthDate
        [Fact]
        public void Validate_BirthDateInFuture_ShouldHaveError()
        {
            DateTime futureDate = DateTime.UtcNow.AddDays(1);
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67",
                BirthDate = futureDate
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.BirthDate);
        }

        [Fact]
        public void Validate_BirthDateInPast_ShouldNotHaveError()
        {
            DateTime pastDate = DateTime.UtcNow.AddYears(-20);
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67",
                BirthDate = pastDate
            };
            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.BirthDate);
        }

        // Contact required
        [Fact]
        public void Validate_NoContactProvided_ShouldHaveError()
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = null,
                Email = null,
                Telegram = null
            };

            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_ContactProvided_ShouldNotHaveError()
        {
            CreateClientCommand command = new CreateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67"
            };

            TestValidationResult<CreateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }
    }
}