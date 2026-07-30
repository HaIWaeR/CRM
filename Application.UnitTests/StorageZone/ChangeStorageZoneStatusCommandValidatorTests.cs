using Application.Behavior.StorageZone;
using Application.Validators.StorageZone;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.StorageZone.Validators
{
    public class ChangeStorageZoneStatusCommandValidatorTests
    {
        private readonly ChangeStorageZoneStatusCommandValidator validator = new();

        [Theory]
        [InlineData(StorageZoneStatus.Empty)]
        [InlineData(StorageZoneStatus.PartiallyOccupied)]
        [InlineData(StorageZoneStatus.FullyOccupied)]
        [InlineData(StorageZoneStatus.Maintenance)]
        public void Validate_StatusIsValid_ShouldNotHaveError(StorageZoneStatus status)
        {
            ChangeStorageZoneStatusCommand command = new ChangeStorageZoneStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeStorageZoneStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((StorageZoneStatus)(-1))]
        [InlineData((StorageZoneStatus)0)]
        [InlineData((StorageZoneStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(StorageZoneStatus status)
        {
            ChangeStorageZoneStatusCommand command = new ChangeStorageZoneStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeStorageZoneStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}