using Application.Behavior.Supplier;
using Application.Validators.Supplier;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Supplier.Validators
{
    public class ChangeSupplierStatusCommandValidatorTests
    {
        private readonly ChangeSupplierStatusCommandValidator validator = new();

        [Theory]
        [InlineData(SupplierStatus.Active)]
        [InlineData(SupplierStatus.Inactive)]
        [InlineData(SupplierStatus.UnderReview)]
        public void Validate_StatusIsValid_ShouldNotHaveError(SupplierStatus status)
        {
            ChangeSupplierStatusCommand command = new ChangeSupplierStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeSupplierStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((SupplierStatus)(-1))]
        [InlineData((SupplierStatus)0)]
        [InlineData((SupplierStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(SupplierStatus status)
        {
            ChangeSupplierStatusCommand command = new ChangeSupplierStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeSupplierStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}