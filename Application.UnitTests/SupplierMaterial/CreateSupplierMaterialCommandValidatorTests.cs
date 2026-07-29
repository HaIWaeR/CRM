using Application.Behavior.SupplierMaterial;
using Application.Validators.SupplierMaterial;
using FluentValidation.TestHelper;

namespace Application.UnitTests.SupplierMaterial.Validators
{
    public class CreateSupplierMaterialCommandValidatorTests
    {
        private readonly CreateSupplierMaterialCommandValidator validator = new();

        // SupplierId
        [Fact]
        public void Validate_SupplierIdIsEmpty_ShouldHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { SupplierId = Guid.Empty };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.SupplierId);
        }

        [Fact]
        public void Validate_SupplierIdIsValid_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { SupplierId = Guid.NewGuid() };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.SupplierId);
        }

        // MaterialId
        [Fact]
        public void Validate_MaterialIdIsEmpty_ShouldHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { MaterialId = Guid.Empty };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.MaterialId);
        }

        [Fact]
        public void Validate_MaterialIdIsValid_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { MaterialId = Guid.NewGuid() };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MaterialId);
        }

        // PriceUnit
        [Fact]
        public void Validate_PriceUnitIsNegative_ShouldHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { PriceUnit = -1 };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsZero_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { PriceUnit = 0 };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsValid_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { PriceUnit = 100 };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsNull_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { PriceUnit = null };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        // DeliveryDays
        [Fact]
        public void Validate_DeliveryDaysIsZero_ShouldHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { DeliveryDays = 0 };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.DeliveryDays);
        }

        [Fact]
        public void Validate_DeliveryDaysIsNegative_ShouldHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { DeliveryDays = -1 };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.DeliveryDays);
        }

        [Fact]
        public void Validate_DeliveryDaysIsValid_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { DeliveryDays = 5 };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.DeliveryDays);
        }

        [Fact]
        public void Validate_DeliveryDaysIsNull_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { DeliveryDays = null };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.DeliveryDays);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { Description = longDescription };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { Description = "Поставка каждую пятницу" };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            CreateSupplierMaterialCommand command = new CreateSupplierMaterialCommand { Description = "" };
            TestValidationResult<CreateSupplierMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}