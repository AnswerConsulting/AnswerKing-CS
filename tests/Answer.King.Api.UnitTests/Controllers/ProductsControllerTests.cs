﻿using Answer.King.Api.Controllers;
using Answer.King.Api.Services;
using Answer.King.Test.Common.CustomAsserts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Answer.King.Api.UnitTests.Controllers;

public class ProductsControllerTests
{
    #region GenericControllerTests

    [Fact]
    public void Controller_RouteAttribute_IsPresentWithCorrectRoute()
    {
        // Assert
        AssertController.HasRouteAttribute<ProductsController>("api/[controller]");
        Assert.Equal(nameof(ProductsController), "ProductsController");
    }

    #endregion GenericControllerTests

    #region GetAll

    [Fact]
    public void GetAll_Method_HasCorrectVerbAttribute()
    {
        // Assert
        AssertController.MethodHasVerb<ProductsController, HttpGetAttribute>(
            nameof(ProductsController.GetAll));
    }

    [Fact]
    public async void GetAll_ValidRequest_ReturnsOkObjectResult()
    {
        // Act
        var result = await GetSubjectUnderTest.GetAll();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    #endregion GetAll

    #region GetOne

    [Fact]
    public void GetOne_Method_HasCorrectVerbAttribute()
    {
        // Assert
        AssertController.MethodHasVerb<ProductsController, HttpGetAttribute>(
            nameof(ProductsController.GetOne));
    }

    [Fact]
    public async void GetOne_ServiceReturnsNull_ReturnsNotFoundResult()
    {
        // Arrange
        var id = Guid.Parse("220D4AF3-A520-4F71-A8BC-519C70867D20");

        // Act
        var result = await GetSubjectUnderTest.GetOne(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact(Skip = "Unfinished test (WIP)")]
    public void GetOne_ValidRequest_ReturnsOkObjectResult()
    {
        // Arrange
        //var id = Guid.Parse("220D4AF3-A520-4F71-A8BC-519C70867D20");
        //var products = new List<Product>();
        //ProductService.GetProduct(id).Returns(products);

        // Act
        //var result = await GetSubjectUnderTest.GetOne(id);

        // Assert
        //Assert.IsType<OkObjectResult>(result);
    }

    #endregion GetOne

    #region Post

    [Fact]
    public void Post_Method_HasCorrectVerbAttribute()
    {
        // Assert
        AssertController.MethodHasVerb<ProductsController, HttpPostAttribute>(
            nameof(ProductsController.Post));
    }

    #endregion Post

    #region Put

    [Fact]
    public void Put_Method_HasCorrectVerbAttribute()
    {
        // Assert
        AssertController.MethodHasVerb<ProductsController, HttpPutAttribute>(
            nameof(ProductsController.Put), "{id}");
    }

    #endregion Put

    #region Retire

    [Fact]
    public void Retire_Method_HasCorrectVerbAttribute()
    {
        // Assert
        AssertController.MethodHasVerb<ProductsController, HttpDeleteAttribute>(
            nameof(ProductsController.Retire), "{id}");
    }

    #endregion Retire

    #region Setup

    private static readonly IProductService ProductService = Substitute.For<IProductService>();

    private static readonly ProductsController GetSubjectUnderTest = new ProductsController(ProductService);

    #endregion Setup
}