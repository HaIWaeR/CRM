using Application.Behavior.StockItem;
using Application.Validators.StockItem;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StockItem.Validators
{
    public class UpdateStockItemCommandValidatorTests
    {
        private readonly UpdateStockItemCommandValidator validator = new();

        [Fact]
        public void Validate_QuantityIsZero_ShouldHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 0,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsNegative_ShouldHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = -5,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsValid_ShouldNotHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_WarehouseIdIsEmpty_ShouldHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.Empty,
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.WarehouseId);
        }

        [Fact]
        public void Validate_WarehouseIdIsValid_ShouldNotHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.WarehouseId);
        }

        [Fact]
        public void Validate_NoProductIdAndNoMaterialId_ShouldHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = null,
                MaterialId = null
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_ProductIdIsValid_ShouldNotHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_MaterialIdIsValid_ShouldNotHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                MaterialId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_BothProductIdAndMaterialId_ShouldHaveError()
        {
            UpdateStockItemCommand command = new UpdateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                MaterialId = Guid.NewGuid()
            };

            TestValidationResult<UpdateStockItemCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x);
        }
    }
}