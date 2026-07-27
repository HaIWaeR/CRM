using Application.Behavior.Product;
using Application.Validators.Product;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Product.Validators
{
    public class UpdateProductCommandValidatorTests
    {
        private readonly UpdateProductCommandValidator validator = new();

        // Name
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_NameIsInvalid_ShouldHaveError(string name)
        {
            UpdateProductCommand command = new UpdateProductCommand { Name = name };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameExceedsMaxLength_ShouldHaveError()
        {
            string longName = new string('a', 201);
            UpdateProductCommand command = new UpdateProductCommand { Name = longName };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_NameIsValid_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Name = "Мягкая игрушка Мишка" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        // Price
        [Fact]
        public void Validate_PriceIsNegative_ShouldHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Price = -100 };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Validate_PriceIsZero_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Price = 0 };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Validate_PriceIsValid_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Price = 1500 };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        // Category
        [Fact]
        public void Validate_CategoryExceedsMaxLength_ShouldHaveError()
        {
            string longCategory = new string('a', 51);
            UpdateProductCommand command = new UpdateProductCommand { Category = longCategory };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Category);
        }

        [Fact]
        public void Validate_CategoryIsValid_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Category = "Игрушки" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Category);
        }

        [Fact]
        public void Validate_CategoryIsEmpty_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Category = "" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Category);
        }

        // Article
        [Fact]
        public void Validate_ArticleWithInvalidCharacters_ShouldHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Article = "TOY 001" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleExceedsMaxLength_ShouldHaveError()
        {
            string longArticle = new string('a', 51);
            UpdateProductCommand command = new UpdateProductCommand { Article = longArticle };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleIsValid_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Article = "TOY-BEAR-001" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Article);
        }

        [Fact]
        public void Validate_ArticleIsEmpty_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Article = "" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Article);
        }

        // Description
        [Fact]
        public void Validate_DescriptionExceedsMaxLength_ShouldHaveError()
        {
            string longDescription = new string('a', 501);
            UpdateProductCommand command = new UpdateProductCommand { Description = longDescription };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsValid_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Description = "Мягкая игрушка" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Validate_DescriptionIsEmpty_ShouldNotHaveError()
        {
            UpdateProductCommand command = new UpdateProductCommand { Description = "" };
            TestValidationResult<UpdateProductCommand> result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}