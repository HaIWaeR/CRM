using Application.Behavior.Product;
using Application.Validators.Product;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Product.Validators
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            CreateProductCommand command = new CreateProductCommand { Name = name };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            CreateProductCommand command = new CreateProductCommand { Name = longName };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Name = "Мягкая игрушка Мишка" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Price
        [Fact]
        public void Validate_PriceIsNegative_ShouldHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Price = -100 };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Validate_PriceIsZero_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Price = 0 };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Validate_PriceIsValid_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Price = 1500 };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        // Categoty
        [Fact]
        public void Validate_CategoryExceedsMaxLength_ShouldHaveError()
        {
            string longCategory = new string('a', 51);
            CreateProductCommand command = new CreateProductCommand { Category = longCategory };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Category);
        }

        [Fact]
        public void Validate_CategoryIsValid_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Category = "Игрушки" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Category);
        }

        [Fact]
        public void Validate_CategoryIsEmpty_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Category = "" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Category);
        }

        // Article
        [Fact]
        public void Validate_ArticleWithInvalidCharacters_ShouldHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Article = "TOY 001" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleExceedsMaxLength_ShouldHaveError()
        {
            string longArticle = new string('a', 51);
            CreateProductCommand command = new CreateProductCommand { Article = longArticle };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleIsValid_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Article = "TOY-BEAR-001" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleIsEmpty_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Article = "" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Article);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            CreateProductCommand command = new CreateProductCommand { Description = longDescription };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Description = "Мягкая игрушка" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            CreateProductCommand command = new CreateProductCommand { Description = "" };
            TestValidationResult<CreateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}