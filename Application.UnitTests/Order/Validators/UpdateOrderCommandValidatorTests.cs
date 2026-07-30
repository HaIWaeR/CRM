using Application.Behavior.Orders;
using Application.Validators.Order;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Order.Validators
{
    public class UpdateOrderCommandValidatorTests
    {
        private readonly UpdateOrderCommandValidator validator = new();

        // ServiceName
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_ServiceNameIsInvalid_ShouldHaveError(string serviceName)
        {
            UpdateOrderCommand command = new UpdateOrderCommand { ServiceName = serviceName };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ServiceName);
        }

        [Fact]
        public void Validate_ServiceNameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateOrderCommand command = new UpdateOrderCommand { ServiceName = longName };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ServiceName);
        }

        [Fact]
        public void Validate_ServiceNameIsValid_ShouldNotHaveError()
        {
            UpdateOrderCommand command = new UpdateOrderCommand { ServiceName = "Монтаж москитной сетки" };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ServiceName);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateOrderCommand command = new UpdateOrderCommand { Description = longDescription };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateOrderCommand command = new UpdateOrderCommand { Description = "Срочный заказ" };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            UpdateOrderCommand command = new UpdateOrderCommand { Description = "" };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // Address
        [Fact]
        public void Validate_AddressExceedsMaxLength_ShouldHaveError()
        {
            string longAddress = new string('a', 301);
            UpdateOrderCommand command = new UpdateOrderCommand { Address = longAddress };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsValid_ShouldNotHaveError()
        {
            UpdateOrderCommand command = new UpdateOrderCommand { Address = "г. Москва, ул. Ленина, д. 1" };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Validate_AddressIsEmpty_ShouldNotHaveError()
        {
            UpdateOrderCommand command = new UpdateOrderCommand { Address = "" };
            TestValidationResult<UpdateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
        }
    }
}