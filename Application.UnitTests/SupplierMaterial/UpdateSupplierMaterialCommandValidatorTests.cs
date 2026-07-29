using Application.Behavior.SupplierMaterial;
using Application.Validators.SupplierMaterial;
using FluentValidation.TestHelper;

namespace Application.UnitTests.SupplierMaterial.Validators
{
    public class UpdateSupplierMaterialCommandValidatorTests
    {
        private readonly UpdateSupplierMaterialCommandValidator validator = new();

        // SupplierId
        [Fact]
        public void Validate_SupplierIdIsEmpty_ShouldHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { SupplierId = Guid.Empty };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.SupplierId);
        }

        [Fact]
        public void Validate_SupplierIdIsValid_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { SupplierId = Guid.NewGuid() };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.SupplierId);
        }

        // MaterialId
        [Fact]
        public void Validate_MaterialIdIsEmpty_ShouldHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { MaterialId = Guid.Empty };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.MaterialId);
        }

        [Fact]
        public void Validate_MaterialIdIsValid_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { MaterialId = Guid.NewGuid() };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MaterialId);
        }

        // PriceUnit
        [Fact]
        public void Validate_PriceUnitIsNegative_ShouldHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { PriceUnit = -1 };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsZero_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { PriceUnit = 0 };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsValid_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { PriceUnit = 100 };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsNull_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { PriceUnit = null };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        // DeliveryDays
        [Fact]
        public void Validate_DeliveryDaysIsZero_ShouldHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { DeliveryDays = 0 };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.DeliveryDays);
        }

        [Fact]
        public void Validate_DeliveryDaysIsNegative_ShouldHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { DeliveryDays = -1 };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.DeliveryDays);
        }

        [Fact]
        public void Validate_DeliveryDaysIsValid_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { DeliveryDays = 5 };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.DeliveryDays);
        }

        [Fact]
        public void Validate_DeliveryDaysIsNull_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { DeliveryDays = null };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.DeliveryDays);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { Description = longDescription };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { Description = "Поставка каждую пятницу" };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            UpdateSupplierMaterialCommand command = new UpdateSupplierMaterialCommand { Description = "" };
            TestValidationResult<UpdateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}