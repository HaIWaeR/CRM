using Application.Behavior.Order;
using Application.Validators.Order;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Order.Validators
{
    public class ChangeOrderStatusCommandValidatorTests
    {
        private readonly ChangeOrderStatusCommandValidator validator = new();

        [Theory]
        [InlineData(OrderStatus.New)]
        [InlineData(OrderStatus.Accepted)]
        [InlineData(OrderStatus.InProgress)]
        [InlineData(OrderStatus.Completed)]
        [InlineData(OrderStatus.Cancelled)]
        public void Validate_StatusIsValid_ShouldNotHaveError(OrderStatus status)
        {
            ChangeOrderStatusCommand command = new ChangeOrderStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeOrderStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((OrderStatus)(-1))]
        [InlineData((OrderStatus)0)]
        [InlineData((OrderStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(OrderStatus status)
        {
            ChangeOrderStatusCommand command = new ChangeOrderStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeOrderStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}