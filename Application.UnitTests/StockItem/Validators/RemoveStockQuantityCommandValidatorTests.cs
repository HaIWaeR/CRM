using Application.Behavior.StockItem;
using Application.Validators.StockItem;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StockItem.Validators
{
    public class RemoveStockQuantityCommandValidatorTests
    {
        private readonly RemoveStockQuantityCommandValidator validator = new();

        [Fact]
        public void Validate_QuantityIsZero_ShouldHaveError()
        {
            RemoveStockQuantityCommand command = new RemoveStockQuantityCommand
            {
                Id = Guid.NewGuid(),
                Quantity = 0
            };

            TestValidationResult<RemoveStockQuantityCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsNegative_ShouldHaveError()
        {
            RemoveStockQuantityCommand command = new RemoveStockQuantityCommand
            {
                Id = Guid.NewGuid(),
                Quantity = -5
            };

            TestValidationResult<RemoveStockQuantityCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsValid_ShouldNotHaveError()
        {
            RemoveStockQuantityCommand command = new RemoveStockQuantityCommand
            {
                Id = Guid.NewGuid(),
                Quantity = 10
            };

            TestValidationResult<RemoveStockQuantityCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
        }
    }
}