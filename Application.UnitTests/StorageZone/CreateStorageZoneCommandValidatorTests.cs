using Application.Behavior.StorageZone;
using Application.Validators.StorageZone;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StorageZone.Validators
{
    public class CreateStorageZoneCommandValidatorTests
    {
        private readonly CreateStorageZoneCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Name = name };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Name = longName };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Name = "Стеллаж для сетки" };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Code
        [Fact]
        public void Validate_CodeWithInvalidCharacters_ShouldHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Code = "STL MESH 01" };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Validate_CodeExceedsMaxLength_ShouldHaveError()
        {
            string longCode = new string('a', 51);
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Code = longCode };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Validate_CodeIsValid_ShouldNotHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Code = "STL-MESH-01" };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Validate_CodeIsNull_ShouldNotHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Code = null };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        // WarehouseId
        [Fact]
        public void Validate_WarehouseIdIsEmpty_ShouldHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { WarehouseId = Guid.Empty };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.WarehouseId);
        }

        [Fact]
        public void Validate_WarehouseIdIsValid_ShouldNotHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { WarehouseId = Guid.NewGuid() };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.WarehouseId);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Description = longDescription };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { Description = "Зона для хранения сетки" };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // MaxCapacity
        [Fact]
        public void Validate_MaxCapacityIsNegative_ShouldHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { MaxCapacity = -1 };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.MaxCapacity);
        }

        [Fact]
        public void Validate_MaxCapacityIsZero_ShouldNotHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { MaxCapacity = 1 };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MaxCapacity);
        }

        [Fact]
        public void Validate_MaxCapacityIsValid_ShouldNotHaveError()
        {
            CreateStorageZoneCommand command = new CreateStorageZoneCommand { MaxCapacity = 100 };
            TestValidationResult<CreateStorageZoneCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.MaxCapacity);
        }
    }
}