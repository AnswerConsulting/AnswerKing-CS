﻿using Answer.King.Test.Common.CustomTraits;
using Answer.King.Domain.Orders.Models;
using Xunit;
using Product = Answer.King.Domain.Orders.Models.Product;

namespace Answer.King.Domain.UnitTests.Orders.Models;

[TestCategory(TestType.Unit)]
public class ProductTests
{
    [Fact]
    public void Product_InitWithDefaultId_ThrowsDefaultValueException()
    {
        // Arrange
        var id = 0;
        var name = "name";
        var description = "description";
        var price = 142;
        var category = new Category(1, "name", "description");

        // Act / Assert

        Assert.Throws<Guard.DefaultValueException>(() => new Product(
            id,
            name,
            description,
            price,
            category)
        );
    }

    [Fact]
    public void Product_InitWithNegativePrice_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var id = 1;
        var name = "name";
        var description = "description";
        var price = -1;
        var category = new Category(1, "name", "description");

        // Act Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Product(
            id,
            name,
            description,
            price,
            category)
        );
    }
}
