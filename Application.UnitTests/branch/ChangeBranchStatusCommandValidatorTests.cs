using Application.Behavior.Branch;
using Application.Validators;
using Domain.Enums;
using FluentValidation.TestHelper;

public class ChangeBranchStatusCommandValidatorTests
{
    private readonly ChangeBranchStatusCommandValidator validator = new();

    [Theory]
    [InlineData(BranchStatus.Active)]
    [InlineData(BranchStatus.Maintenance)]
    public void ChangeBranchStatusCommandValidator_StatusIsValid_ShouldNotReturnValidationError(BranchStatus status)
    {
        ChangeBranchStatusCommand command = new ChangeBranchStatusCommand { Id = Guid.NewGuid(), Status = status };

        TestValidationResult<ChangeBranchStatusCommand> result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Status);
    }

    [Theory]
    [InlineData((BranchStatus)(-1))]
    [InlineData((BranchStatus)0)]
    [InlineData((BranchStatus)999)]
    public void ChangeBranchStatusCommandValidator_StatusIsInvalid_ReturnValidationError(BranchStatus status)
    {
        ChangeBranchStatusCommand command = new ChangeBranchStatusCommand { Id = Guid.NewGuid(), Status = status };

        TestValidationResult<ChangeBranchStatusCommand> result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    [Fact]
    public void ChangeBranchStatusCommandValidator_IdIsValid_ShouldNotReturnValidationError()
    {
        ChangeBranchStatusCommand command = new ChangeBranchStatusCommand { Id = Guid.NewGuid(), Status = BranchStatus.Active };

        TestValidationResult<ChangeBranchStatusCommand> result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void ChangeBranchStatusCommandValidator_IdIsEmpty_ReturnValidationError()
    {
        ChangeBranchStatusCommand command = new ChangeBranchStatusCommand { Id = Guid.Empty, Status = BranchStatus.Active };

        TestValidationResult<ChangeBranchStatusCommand> result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}