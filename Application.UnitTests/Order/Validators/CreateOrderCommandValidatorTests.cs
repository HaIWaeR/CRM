using Application.Behavior.Orders;
using Application.Validators.Order;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Order.Validators
{
    public class CreateOrderCommandValidatorTests
    {
        private readonly CreateOrderCommandValidator validator = new();

        // ServiceName
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_ServiceNameIsInvalid_ShouldHaveError(string serviceName)
        {
            CreateOrderCommand command = new CreateOrderCommand { ServiceName = serviceName };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ServiceName);
        }

        [Fact]
        public void Validate_ServiceNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            CreateOrderCommand command = new CreateOrderCommand { ServiceName = longName };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ServiceName);
        }

        [Fact]
        public void Validate_ServiceNameIsValid_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { ServiceName = "Тайский массаж" };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ServiceName);
        }

        // Price
        [Fact]
        public void Validate_PriceIsZero_ShouldHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { Price = 0 };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Validate_PriceIsNegative_ShouldHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { Price = -100 };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Validate_PriceIsValid_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { Price = 4600 };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateOrderCommand command = new CreateOrderCommand { Description = longDescription };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        // Позитивный
        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { Description = "Срочно" };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { Description = "" };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            CreateOrderCommand command = new CreateOrderCommand { Address = longAddress };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { Address = "Самара, Куйбышева 17" };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsEmpty_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand { Address = "" };
            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }
    }
}