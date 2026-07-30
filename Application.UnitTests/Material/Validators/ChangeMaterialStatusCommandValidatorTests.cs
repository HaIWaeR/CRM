using Application.Behavior.Material;
using Application.Validators.Material;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Material.Validators
{
    public class ChangeMaterialStatusCommandValidatorTests
    {
        private readonly ChangeMaterialStatusCommandValidator validator = new();

        [Theory]
        [InlineData(MaterialStatus.Active)]
        [InlineData(MaterialStatus.Inactive)]
        [InlineData(MaterialStatus.Pending)]
        [InlineData(MaterialStatus.OutOfStock)]
        public void Validate_StatusIsValid_ShouldNotHaveError(MaterialStatus status)
        {
            ChangeMaterialStatusCommand command = new ChangeMaterialStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeMaterialStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((MaterialStatus)(-1))]
        [InlineData((MaterialStatus)0)]
        [InlineData((MaterialStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(MaterialStatus status)
        {
            ChangeMaterialStatusCommand command = new ChangeMaterialStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeMaterialStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}