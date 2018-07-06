﻿using Answer.King.Domain.Repositories.Models;
using Answer.King.Test.Common.CustomTraits;
using System;
using Answer.King.Infrastructure.Repositories.Mappings;
using Xunit;

namespace Answer.King.Domain.UnitTests.Repositories.Models
{
    [TestCategory(TestType.Unit)]
    public class ProductTests
    {
        [Fact]
        public void Product_InitWithDefaultGuid_ThrowsDefaultValueException()
        {
            // Arrange
            var id = default(Guid);
            var productName = "Product Name";
            var productDescription = "Product Description";
            var category = this.GetCategory();
            var price = 142;
            var retired = false;

            // Act / Assert

            Assert.Throws<Guard.DefaultValueException>(() => ProductFactory.CreateProduct(
                id,
                productName,
                productDescription,
                price,
                category,
                retired)
            );
        }

        [Fact]
        public void Product_InitWithNullName_ThrowsArgumentNullException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var productName = (null as string);
            var productDescription = "Product Description";
            var category = this.GetCategory();
            var price = 142;
            var retired = false;

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => ProductFactory.CreateProduct(
                id,
                productName,
                productDescription,
                price,
                category,
                retired)
            );
        }

        [Fact]
        public void Product_InitWithEmptyStringName_ThrowsEmptyStringException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var productName = "";
            var productDescription = "Product Description";
            var category = this.GetCategory();
            var price = 142;
            var retired = false;

            // Act / Assert
            Assert.Throws<Guard.EmptyStringException>(() => ProductFactory.CreateProduct(
                id,
                productName,
                productDescription,
                price,
                category,
                retired)
           );
        }

        [Fact]
        public void Product_InitWithNullDescription_ThrowsArgumentNullException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var productName = "Product Name";
            var productDescription = (null as string);
            var category = this.GetCategory();
            var price = 142;
            var retired = false;

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => ProductFactory.CreateProduct(
                id,
                productName,
                productDescription,
                price,
                category,
                retired)
            );
        }

        [Fact]
        public void Product_InitWithEmptyStringDescription_ThrowsEmptyStringException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var productName = "Product Name";
            var productDescription = "";
            var category = this.GetCategory();
            var price = 142;
            var retired = false;

            // Act / Assert
            Assert.Throws<Guard.EmptyStringException>(() => ProductFactory.CreateProduct(
                id,
                productName,
                productDescription,
                price,
                category,
                retired)
            );
        }

        [Fact]
        public void Product_InitWithNullCategory_ThrowsNullArgumentException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var productName = "Product Name";
            var productDescription = "Product Description";
            var category = (null as CategoryId);
            var price = 142;
            var retired = false;

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => ProductFactory.CreateProduct(
                id,
                productName,
                productDescription,
                price,
                category,
                retired)
            );
        }

        [Fact]
        public void Product_InitWithNegativePrice_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var productName = "Product Name";
            var productDescription = "Product Description";
            var category = this.GetCategory();
            var price = -1;
            var retired = false;

            // Act Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => ProductFactory.CreateProduct(
                id,
                productName,
                productDescription,
                price,
                category,
                retired)
            );
        }

        #region Helpers

        private CategoryId GetCategory() => new CategoryId(
            Guid.NewGuid()
        );

        #endregion Helpers
    }
}