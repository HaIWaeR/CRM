using Application.Behavior.Material;
using Application.Validators.Material;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Material.Validators
{
    public class UpdateMaterialCommandValidatorTests
    {
        private readonly UpdateMaterialCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { Name = name };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateMaterialCommand command = new UpdateMaterialCommand { Name = longName };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { Name = "Хлопковая ткань" };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Article
        [Fact]
        public void Validate_ArticleWithInvalidCharacters_ShouldHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { Article = "FAB 001" };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleExceedsMaxLength_ShouldHaveError()
        {
            string longArticle = new string('a', 51);
            UpdateMaterialCommand command = new UpdateMaterialCommand { Article = longArticle };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleIsValid_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { Article = "FAB-001" };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Article);
        }

        // CategoryCode
        [Fact]
        public void Validate_CategoryCodeExceedsMaxLength_ShouldHaveError()
        {
            string longCategory = new string('a', 21);
            UpdateMaterialCommand command = new UpdateMaterialCommand { CategoryCode = longCategory };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.CategoryCode);
        }

        [Fact]
        public void Validate_CategoryCodeIsValid_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { CategoryCode = "Ткани" };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.CategoryCode);
        }

        // PriceUnit
        [Fact]
        public void Validate_PriceUnitIsNegative_ShouldHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { PriceUnit = -1 };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsZero_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { PriceUnit = 0 };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        // Weight
        [Fact]
        public void Validate_WeightIsNegative_ShouldHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { Weight = -1 };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact]
        public void Validate_WeightIsZero_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { Weight = 0 };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        // UnitMeasurement
        [Fact]
        public void Validate_UnitMeasurementIsInvalid_ShouldHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { UnitMeasurement = (UnitMeasurement)999 };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UnitMeasurement);
        }

        [Fact]
        public void Validate_UnitMeasurementIsValid_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { UnitMeasurement = UnitMeasurement.Meter };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.UnitMeasurement);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateMaterialCommand command = new UpdateMaterialCommand { Description = longDescription };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { Description = "Хороший материал" };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        // AdditionInformation
        [Fact]
        public void Validate_AdditionInformationExceedsMaxLength_ShouldHaveError()
        {
            string longInfo = new string('a', 501);
            UpdateMaterialCommand command = new UpdateMaterialCommand { AdditionInformation = longInfo };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.AdditionInformation);
        }

        [Fact]
        public void Validate_AdditionInformationIsValid_ShouldNotHaveError()
        {
            UpdateMaterialCommand command = new UpdateMaterialCommand { AdditionInformation = "Сорт 1" };
            TestValidationResult<UpdateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.AdditionInformation);
        }
    }
}