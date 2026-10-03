using Application.Behavior.User;
using Application.Validators.User;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.User.Validators
{
    public class ChangeUserRoleCommandValidatorTests
    {
        private readonly ChangeUserRoleCommandValidator validator = new();

        [Theory]
        [InlineData(UserRole.Admin)]
        [InlineData(UserRole.Owner)]
        [InlineData(UserRole.Reader)]
        public void Validate_RoleIsValid_ShouldNotHaveError(UserRole role)
        {
            ChangeUserRoleCommand command = new ChangeUserRoleCommand
            {
                Id = Guid.NewGuid(),
                Role = role
            };

            TestValidationResult<ChangeUserRoleCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Role);
        }

        [Theory]
        [InlineData((UserRole)(-1))]
        [InlineData((UserRole)0)]
        [InlineData((UserRole)99)]
        public void Validate_RoleIsInvalid_ShouldHaveError(UserRole role)
        {
            ChangeUserRoleCommand command = new ChangeUserRoleCommand
            {
                Id = Guid.NewGuid(),
                Role = role
            };

            TestValidationResult<ChangeUserRoleCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Role);
        }
    }
}