using Application.Behavior.StockItem;
using Application.Validators.StockItem;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StockItem.Validators
{
    public class CreateStockItemCommandValidatorTests
    {
        private readonly CreateStockItemCommandValidator validator = new();

        // Quantity
        [Fact]
        public void Validate_QuantityIsZero_ShouldHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 0,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsNegative_ShouldHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = -5,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_QuantityIsValid_ShouldNotHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
        }

        // WarehouseId
        [Fact]
        public void Validate_WarehouseIdIsEmpty_ShouldHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.Empty,
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.WarehouseId);
        }

        [Fact]
        public void Validate_WarehouseIdIsValid_ShouldNotHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.WarehouseId);
        }

        // ProductId or MaterialId
        [Fact]
        public void Validate_NoProductIdAndNoMaterialId_ShouldHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = null,
                MaterialId = null
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_ProductIdIsValid_ShouldNotHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_MaterialIdIsValid_ShouldNotHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                MaterialId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Validate_BothProductIdAndMaterialId_ShouldHaveError()
        {
            CreateStockItemCommand command = new CreateStockItemCommand
            {
                Quantity = 10,
                WarehouseId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                MaterialId = Guid.NewGuid()
            };

            TestValidationResult<CreateStockItemCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x);
        }
    }
}