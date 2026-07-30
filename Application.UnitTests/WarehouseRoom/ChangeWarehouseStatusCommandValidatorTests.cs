using Application.Behavior.WarehouseRoom;
using Application.Validators.WarehouseRoom;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.WarehouseRoom.Validators
{
    public class ChangeWarehouseStatusCommandValidatorTests
    {
        private readonly ChangeWarehouseStatusCommandValidator validator = new();

        [Theory]
        [InlineData(WarehouseStatus.Active)]
        [InlineData(WarehouseStatus.Inactive)]
        [InlineData(WarehouseStatus.Maintenance)]
        public void Validate_StatusIsValid_ShouldNotHaveError(WarehouseStatus status)
        {
            ChangeWarehouseStatusCommand command = new ChangeWarehouseStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeWarehouseStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((WarehouseStatus)(-1))]
        [InlineData((WarehouseStatus)0)]
        [InlineData((WarehouseStatus)14)]
        public void Validate_StatusIsInvalid_ShouldHaveError(WarehouseStatus status)
        {
            ChangeWarehouseStatusCommand command = new ChangeWarehouseStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeWarehouseStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}