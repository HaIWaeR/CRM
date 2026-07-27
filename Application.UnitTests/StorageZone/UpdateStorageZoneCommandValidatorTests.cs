using Application.Behavior.StorageZone;
using Application.Validators.StorageZone;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StorageZone.Validators
{
    public class UpdateStorageZoneCommandValidatorTests
    {
        private readonly UpdateStorageZoneCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Name = name };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Name = longName };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Name = "Стеллаж для сетки" };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Code
        [Fact]
        public void Validate_CodeWithInvalidCharacters_ShouldHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Code = "STL MESH 01" };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Validate_CodeExceedsMaxLength_ShouldHaveError()
        {
            string longCode = new string('a', 51);
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Code = longCode };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Validate_CodeIsValid_ShouldNotHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Code = "STL-MESH-01" };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Validate_CodeIsNull_ShouldNotHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Code = null };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        // WarehouseId
        [Fact]
        public void Validate_WarehouseIdIsEmpty_ShouldHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { WarehouseId = Guid.Empty };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.WarehouseId);
        }

        [Fact]
        public void Validate_WarehouseIdIsValid_ShouldNotHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { WarehouseId = Guid.NewGuid() };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.WarehouseId);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Description = longDescription };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { Description = "Зона для хранения сетки" };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // MaxCapacity
        [Fact]
        public void Validate_MaxCapacityIsNegative_ShouldHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { MaxCapacity = -1 };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.MaxCapacity);
        }

        [Fact]
        public void Validate_MaxCapacityIsZero_ShouldNotHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { MaxCapacity = 0 };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MaxCapacity);
        }

        [Fact]
        public void Validate_MaxCapacityIsValid_ShouldNotHaveError()
        {
            UpdateStorageZoneCommand command = new UpdateStorageZoneCommand { MaxCapacity = 100 };
            TestValidationResult<UpdateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MaxCapacity);
        }
    }
}