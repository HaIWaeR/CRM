using Application.Behavior.Product;
using Application.Validators.Product;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Product.Validators
{
    public class ChangeProductStatusCommandValidatorTests
    {
        private readonly ChangeProductStatusCommandValidator validator = new();

        [Theory]
        [InlineData(ProductStatus.Active)]
        [InlineData(ProductStatus.Inactive)]
        [InlineData(ProductStatus.OutOfStock)]
        public void Validate_StatusIsValid_ShouldNotHaveError(ProductStatus status)
        {
            ChangeProductStatusCommand command = new ChangeProductStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeProductStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((ProductStatus)(-1))]
        [InlineData((ProductStatus)0)]
        [InlineData((ProductStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(ProductStatus status)
        {
            ChangeProductStatusCommand command = new ChangeProductStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeProductStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}