using Application.Behavior.StockItem;
using Application.Validators.StockItem;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StockItem.Validators
{
    public class AddStockQuantityCommandValidatorTests
    {
        private readonly AddStockQuantityCommandValidator validator = new();

        [Fact]
        public void Validate_QuantityIsZero_ShouldHaveError()
        {
            AddStockQuantityCommand command = new AddStockQuantityCommand
            {
                Id = Guid.NewGuid(),
                Quantity = 0
            };

            TestValidationResult<AddStockQuantityCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsNegative_ShouldHaveError()
        {
            AddStockQuantityCommand command = new AddStockQuantityCommand
            {
                Id = Guid.NewGuid(),
                Quantity = -5
            };

            TestValidationResult<AddStockQuantityCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsValid_ShouldNotHaveError()
        {
            AddStockQuantityCommand command = new AddStockQuantityCommand
            {
                Id = Guid.NewGuid(),
                Quantity = 10
            };

            TestValidationResult<AddStockQuantityCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
        }
    }
}