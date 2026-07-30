using Application.Behavior.Client;
using Application.Validators;
using Application.Validators.Client;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Client.Validators
{
    public class ChangeClientStatusCommandValidatorTests
    {
        private readonly ChangeClientStatusCommandValidator validator = new();

        [Theory]
        [InlineData(ClientStatus.Active)]
        [InlineData(ClientStatus.Inactive)]
        [InlineData(ClientStatus.Draft)]
        public void Validate_StatusIsValid_ShouldNotHaveError(ClientStatus status)
        {
            ChangeClientStatusCommand command = new ChangeClientStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeClientStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((ClientStatus)(-1))]
        [InlineData((ClientStatus)0)]
        [InlineData((ClientStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(ClientStatus status)
        {
            ChangeClientStatusCommand command = new ChangeClientStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeClientStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}