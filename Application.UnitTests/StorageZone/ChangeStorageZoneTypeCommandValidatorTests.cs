using Application.Behavior.StorageZone;
using Application.Validators.StorageZone;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StorageZone.Validators
{
    public class ChangeStorageZoneTypeCommandValidatorTests
    {
        private readonly ChangeStorageZoneTypeCommandValidator validator = new();

        [Theory]
        [InlineData(StorageZoneType.None)]
        [InlineData(StorageZoneType.Product)]
        [InlineData(StorageZoneType.Service)]
        [InlineData(StorageZoneType.Material)]
        public void Validate_TypeIsValid_ShouldNotHaveError(StorageZoneType zoneType)
        {
            ChangeStorageZoneTypeCommand command = new ChangeStorageZoneTypeCommand
            {
                Id = Guid.NewGuid(),
                ZoneType = zoneType
            };

            TestValidationResult<ChangeStorageZoneTypeCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ZoneType);
        }

        [Theory]
        [InlineData((StorageZoneType)(-1))]
        [InlineData((StorageZoneType)0)]
        [InlineData((StorageZoneType)99)]
        public void Validate_TypeIsInvalid_ShouldHaveError(StorageZoneType zoneType)
        {
            ChangeStorageZoneTypeCommand command = new ChangeStorageZoneTypeCommand
            {
                Id = Guid.NewGuid(),
                ZoneType = zoneType
            };

            TestValidationResult<ChangeStorageZoneTypeCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ZoneType);
        }
    }
}