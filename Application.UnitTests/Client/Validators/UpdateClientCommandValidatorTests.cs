using Application.Behavior.Client;
using Application.Validators.Client;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Client.Validators
{
    public class UpdateClientCommandValidatorTests
    {
        private readonly UpdateClientCommandValidator validator = new();

        // FirstName
        [Fact]
        public void Validate_FirstNameIsValid_ShouldNotHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { FirstName = "Виталий" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Validate_FirstNameIsEmpty_ShouldHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { FirstName = "" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Validate_FirstNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            UpdateClientCommand command = new UpdateClientCommand { FirstName = longName };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        // LastName
        [Fact]
        public void Validate_LastNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            UpdateClientCommand command = new UpdateClientCommand { LastName = longName };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Fact]
        public void Validate_LastNameIsValid_ShouldNotHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { LastName = "Папков" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        }

        // MiddleName
        [Fact]
        public void Validate_MiddleNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 101);
            UpdateClientCommand command = new UpdateClientCommand { MiddleName = longName };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.MiddleName);
        }

        [Fact]
        public void Validate_MiddleNameIsValid_ShouldNotHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { MiddleName = "Алексеевич" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MiddleName);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            UpdateClientCommand command = new UpdateClientCommand { Address = longAddress };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { Address = "ул. Ленина, 1" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        // Notes
        [Fact]
        public void Validate_NotesExceedsMaxLength_ShouldHaveError()
        {
            string longNotes = new string('a', 501);
            UpdateClientCommand command = new UpdateClientCommand { Notes = longNotes };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }

        [Fact]
        public void Validate_NotesIsValid_ShouldNotHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { Notes = "Постоянный клиент" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Notes);
        }

        // Telegram
        [Fact]
        public void Validate_TelegramWithoutAtSymbol_ShouldHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { Telegram = "haiwaer" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Telegram);
        }

        [Fact]
        public void Validate_TelegramWithAtSymbol_ShouldNotHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand { Telegram = "@haiwaer" };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Telegram);
        }

        [Fact]
        public void Validate_TelegramExceedsMaxLength_ShouldHaveError()
        {
            string longTelegram = new string('a', 51);
            UpdateClientCommand command = new UpdateClientCommand { Telegram = "@" + longTelegram };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
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
            UpdateClientCommand command = new UpdateClientCommand { Phone = phone };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("abcdef")]
        [InlineData("+7(999)")]
        public void Validate_PhoneIsInvalid_ShouldHaveError(string phone)
        {
            UpdateClientCommand command = new UpdateClientCommand { Phone = phone };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        // Email
        [Theory]
        [InlineData("test@mail.ru")]
        [InlineData("user@domain.com")]
        public void Validate_EmailIsValid_ShouldNotHaveError(string email)
        {
            UpdateClientCommand command = new UpdateClientCommand { Email = email };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        public void Validate_EmailIsInvalid_ShouldHaveError(string email)
        {
            UpdateClientCommand command = new UpdateClientCommand { Email = email };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        // BirthDate
        [Fact]
        public void Validate_BirthDateInFuture_ShouldHaveError()
        {
            DateTime futureDate = DateTime.UtcNow.AddDays(1);
            UpdateClientCommand command = new UpdateClientCommand { BirthDate = futureDate };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.BirthDate);
        }

        [Fact]
        public void Validate_BirthDateInPast_ShouldNotHaveError()
        {
            DateTime pastDate = DateTime.UtcNow.AddYears(-20);
            UpdateClientCommand command = new UpdateClientCommand { BirthDate = pastDate };
            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.BirthDate);
        }

        // Contact required
        [Fact]
        public void Validate_NoContactProvided_ShouldHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand
            {
                FirstName = "Виталий",
                Phone = null,
                Email = null,
                Telegram = null
            };

            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_ContactProvided_ShouldNotHaveError()
        {
            UpdateClientCommand command = new UpdateClientCommand
            {
                FirstName = "Виталий",
                Phone = "+7 999 123 45 67"
            };

            TestValidationResult<UpdateClientCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }
    }
}