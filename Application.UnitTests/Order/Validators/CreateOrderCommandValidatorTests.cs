using Application.Behavior.Orders;
using Application.Validators.Order;
using FluentValidation.TestHelper;
using Shared.DTOs.Order;

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

        //  OrderItems

        [Fact]
        public void Validate_OrderItemsIsEmpty_ShouldHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand
            {
                ServiceName = "Заказ игрушек",
                OrderItems = new List<OrderItemRequestDto>()
            };

            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.OrderItems);
        }

        [Fact]
        public void Validate_OrderItemsHasValidItems_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand
            {
                ServiceName = "Заказ игрушек",
                OrderItems = new List<OrderItemRequestDto>
        {
            new OrderItemRequestDto { ProductId = Guid.NewGuid(), Quantity = 2 }
        }
            };

            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.OrderItems);
        }

        [Fact]
        public void Validate_OrderItemQuantityIsZero_ShouldHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand
            {
                ServiceName = "Заказ игрушек",
                OrderItems = new List<OrderItemRequestDto>
        {
            new OrderItemRequestDto { ProductId = Guid.NewGuid(), Quantity = 0 }
        }
            };

            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.OrderItems);
        }

        [Fact]
        public void Validate_OrderItemQuantityIsNegative_ShouldHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand
            {
                ServiceName = "Заказ игрушек",
                OrderItems = new List<OrderItemRequestDto>
        {
            new OrderItemRequestDto { ProductId = Guid.NewGuid(), Quantity = -1 }
        }
            };

            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.OrderItems);
        }

        [Fact]
        public void Validate_OrderItemProductIdIsEmpty_ShouldHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand
            {
                ServiceName = "Заказ игрушек",
                OrderItems = new List<OrderItemRequestDto>
        {
            new OrderItemRequestDto { ProductId = Guid.Empty, Quantity = 2 }
        }
            };

            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.OrderItems);
        }

        [Fact]
        public void Validate_MultipleOrderItems_ShouldNotHaveError()
        {
            CreateOrderCommand command = new CreateOrderCommand
            {
                ServiceName = "Заказ игрушек",
                OrderItems = new List<OrderItemRequestDto>
        {
            new OrderItemRequestDto { ProductId = Guid.NewGuid(), Quantity = 2 },
            new OrderItemRequestDto { ProductId = Guid.NewGuid(), Quantity = 1 }
        }
            };

            TestValidationResult<CreateOrderCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.OrderItems);
        }
    }
}