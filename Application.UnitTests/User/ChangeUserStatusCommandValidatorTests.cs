using Application.Behavior.User;
using Application.Validators.User;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.User.Validators
{
    public class ChangeUserStatusCommandValidatorTests
    {
        private readonly ChangeUserStatusCommandValidator validator = new();

        [Theory]
        [InlineData(UserStatus.Active)]
        [InlineData(UserStatus.Inactive)]
        [InlineData(UserStatus.Vacation)]
        [InlineData(UserStatus.Fired)]
        public void Validate_StatusIsValid_ShouldNotHaveError(UserStatus status)
        {
            ChangeUserStatusCommand command = new ChangeUserStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeUserStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((UserStatus)(-1))]
        [InlineData((UserStatus)0)]
        [InlineData((UserStatus)99)]
        public void Validate_StatusIsInvalid_ShouldHaveError(UserStatus status)
        {
            ChangeUserStatusCommand command = new ChangeUserStatusCommand
            {
                Id = Guid.NewGuid(),
                Status = status
            };

            TestValidationResult<ChangeUserStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}