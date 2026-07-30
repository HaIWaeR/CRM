using Application.Behavior.Supplier;
using Application.Validators.Supplier;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Supplier.Validators
{
    public class ChangeSupplierTypeCommandValidatorTests
    {
        private readonly ChangeSupplierTypeCommandValidator validator = new();

        [Theory]
        [InlineData(SupplierType.Wholesale)]
        [InlineData(SupplierType.Retail)]
        [InlineData(SupplierType.Manufacturer)]
        [InlineData(SupplierType.Distributors)]
        [InlineData(SupplierType.Importers)]
        public void Validate_TypeIsValid_ShouldNotHaveError(SupplierType supplierType)
        {
            ChangeSupplierTypeCommand command = new ChangeSupplierTypeCommand
            {
                Id = Guid.NewGuid(),
                SupplierType = supplierType
            };

            TestValidationResult<ChangeSupplierTypeCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.SupplierType);
        }

        [Theory]
        [InlineData((SupplierType)(-1))]
        [InlineData((SupplierType)0)]
        [InlineData((SupplierType)99)]
        public void Validate_TypeIsInvalid_ShouldHaveError(SupplierType supplierType)
        {
            ChangeSupplierTypeCommand command = new ChangeSupplierTypeCommand
            {
                Id = Guid.NewGuid(),
                SupplierType = supplierType
            };

            TestValidationResult<ChangeSupplierTypeCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.SupplierType);
        }
    }
}