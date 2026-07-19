using Application.Behavior.Branch;
using Application.Validators;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Branch.Validators
{
    public class ChangeBranchStatusCommandValidatorTests
    {
        private readonly ChangeBranchStatusCommandValidator validator = new();

        [Theory]
        [InlineData(BranchStatus.Active)]
        [InlineData(BranchStatus.Maintenance)]
        public void Validate_StatusIsValid_ShouldNotHaveError(BranchStatus status)
        {
            ChangeBranchStatusCommand command = new ChangeBranchStatusCommand { Id = Guid.NewGuid(), Status = status };
            TestValidationResult<ChangeBranchStatusCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData((BranchStatus)(-1))]
        [InlineData((BranchStatus)0)]
        [InlineData((BranchStatus)999)]
        public void Validate_StatusIsInvalid_ShouldHaveError(BranchStatus status)
        {
            ChangeBranchStatusCommand command = new ChangeBranchStatusCommand { Id = Guid.NewGuid(), Status = status };
            TestValidationResult<ChangeBranchStatusCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}