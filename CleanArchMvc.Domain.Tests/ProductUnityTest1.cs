using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Xunit;
using System;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnityTest1
    {
        [Fact (DisplayName ="Valid Parameters")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "Product image");
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation >();
        }


        [Fact(DisplayName = "Invalid Id Value")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId1()
        {
            Action action = () => new Product(-1, "Product name", "Product description", 9.99m, 99, "Product image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id Value.");

        }

        [Fact(DisplayName = "Short Name Value")]
        public void CreateProduct_ShortNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, "P", "Product description", 9.99m, 99, "Product image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum of 3 characters");

        }

        [Fact(DisplayName = "Too Long Image Name")]
        public void CreateProduct_LongImageNameValue_DomainExceptionInvalidImageName()
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "Product image with 10000000000000000000000" +
                "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum of 250 characters");

        }

        [Fact(DisplayName = "Null image name")]
        public void CreateProduct_ImageNameValueNull_DomainExceptionNullImageName()
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

        }

        [Fact(DisplayName = "Null image name")]
        public void CreateProduct_ImageNameNull_DomainExceptionNullImage()
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
            action.Should().NotThrow<NullReferenceException>();

        }

        [Fact(DisplayName = "Invalid Price Value")]
        public void CreateProduct_InvalidPriceValue_DomainExceptionInvalPrice()
        {
            Action action = () => new Product(1, "Product name", "Product description", -9.99m, 99, "Product image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value");

        }

        [Theory(DisplayName = "Invalid stock Value")]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_DomainExceptionInvalidStockValue(int value)
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, value, "Product image");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value");

        }
    }
}
