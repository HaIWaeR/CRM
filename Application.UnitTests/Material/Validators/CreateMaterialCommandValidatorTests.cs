using Application.Behavior.Material;
using Application.Validators.Material;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Material.Validators
{
    public class CreateMaterialCommandValidatorTests
    {
        private readonly CreateMaterialCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            CreateMaterialCommand command = new CreateMaterialCommand { Name = name };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            CreateMaterialCommand command = new CreateMaterialCommand { Name = longName };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { Name = "Хлопковая ткань" };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Article
        [Fact]
        public void Validate_ArticleWithInvalidCharacters_ShouldHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { Article = "FAB 001" };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleExceedsMaxLength_ShouldHaveError()
        {
            string longArticle = new string('a', 51);
            CreateMaterialCommand command = new CreateMaterialCommand { Article = longArticle };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleIsValid_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { Article = "FAB-001" };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Article);
        }

        // CategoryCode
        [Fact]
        public void Validate_CategoryCodeExceedsMaxLength_ShouldHaveError()
        {
            string longCategory = new string('a', 21);
            CreateMaterialCommand command = new CreateMaterialCommand { CategoryCode = longCategory };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.CategoryCode);
        }

        [Fact]
        public void Validate_CategoryCodeIsValid_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { CategoryCode = "Ткани" };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.CategoryCode);
        }

        // PriceUnit
        [Fact]
        public void Validate_PriceUnitIsNegative_ShouldHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { PriceUnit = -1 };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PriceUnit);
        }

        [Fact]
        public void Validate_PriceUnitIsZero_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { PriceUnit = 0 };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PriceUnit);
        }

        // Attributes
        [Fact]
        public void Validate_AttributesExceedsMaxCount_ShouldHaveError()
        {
            var attributes = new Dictionary<string, string>();
            for (int i = 0; i < 21; i++)
                attributes.Add($"Key{i}", $"Value{i}");

            CreateMaterialCommand command = new CreateMaterialCommand
            {
                Attributes = attributes,
                Name = "Тестовый материал",
                UnitMeasurement = UnitMeasurement.Piece
            };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Attributes);
        }

        [Fact]
        public void Validate_AttributesKeyExceedsMaxLength_ShouldHaveError()
        {
            var attributes = new Dictionary<string, string>
    {
        { new string('a', 51), "value" }
    };

            CreateMaterialCommand command = new CreateMaterialCommand
            {
                Attributes = attributes,
                Name = "Тестовый материал",
                UnitMeasurement = UnitMeasurement.Piece
            };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Attributes);
        }

        [Fact]
        public void Validate_AttributesValueExceedsMaxLength_ShouldHaveError()
        {
            var attributes = new Dictionary<string, string>
    {
        { "key", new string('a', 201) }
    };

            CreateMaterialCommand command = new CreateMaterialCommand
            {
                Attributes = attributes,
                Name = "Тестовый материал",
                UnitMeasurement = UnitMeasurement.Piece
            };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Attributes);
        }

        [Fact]
        public void Validate_AttributesIsValid_ShouldNotHaveError()
        {
            var attributes = new Dictionary<string, string>
    {
        { "Цвет", "чёрный" },
        { "Размер", "XL" },
        { "Материал", "стеклопластик" }
    };

            CreateMaterialCommand command = new CreateMaterialCommand
            {
                Attributes = attributes,
                Name = "Сетка москитная",
                UnitMeasurement = UnitMeasurement.SquareMeter
            };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Attributes);
        }

        [Fact]
        public void Validate_AttributesIsNull_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand
            {
                Attributes = null,
                Name = "Тестовый материал",
                UnitMeasurement = UnitMeasurement.Piece
            };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Attributes);
        }

        [Fact]
        public void Validate_AttributesIsEmpty_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand
            {
                Attributes = new Dictionary<string, string>(),
                Name = "Тестовый материал",
                UnitMeasurement = UnitMeasurement.Piece
            };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Attributes);
        }

        // Weight
        [Fact]
        public void Validate_WeightIsNegative_ShouldHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { Weight = -1 };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact]
        public void Validate_WeightIsZero_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { Weight = 0 };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Weight);
        }

        // UnitMeasurement
        [Fact]
        public void Validate_UnitMeasurementIsInvalid_ShouldHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { UnitMeasurement = (UnitMeasurement)999 };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UnitMeasurement);
        }

        [Fact]
        public void Validate_UnitMeasurementIsValid_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { UnitMeasurement = UnitMeasurement.Meter };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.UnitMeasurement);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateMaterialCommand command = new CreateMaterialCommand { Description = longDescription };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { Description = "Хороший материал" };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }


        // AdditionInformation
        [Fact]
        public void Validate_AdditionInformationExceedsMaxLength_ShouldHaveError()
        {
            string longInfo = new string('a', 501);
            CreateMaterialCommand command = new CreateMaterialCommand { AdditionInformation = longInfo };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.AdditionInformation);
        }

        [Fact]
        public void Validate_AdditionInformationIsValid_ShouldNotHaveError()
        {
            CreateMaterialCommand command = new CreateMaterialCommand { AdditionInformation = "Сорт 1" };
            TestValidationResult<CreateMaterialCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.AdditionInformation);
        }
    }
}