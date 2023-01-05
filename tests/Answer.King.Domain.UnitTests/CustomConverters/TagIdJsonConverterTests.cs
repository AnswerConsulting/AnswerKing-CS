﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Answer.King.Api.Common.JsonConverters;
using Answer.King.Domain.Repositories.Models;
using Answer.King.Test.Common.CustomTraits;
using Xunit;

namespace Answer.King.Domain.UnitTests.CustomConverters;

[TestCategory(TestType.Unit)]
public class TagIdJsonConverterTests
{
    [Fact]
    public void Read_ValidInt64_ReturnsTagId()
    {
        // Arrange
        string json = "1";
        byte[] jsonUtf8Bytes = Encoding.UTF8.GetBytes(json);
        var jsonReader = new Utf8JsonReader(jsonUtf8Bytes);
        jsonReader.Read();

        var tagIdJsonConverter = new TagIdJsonConverter();

        var expected = new TagId(1);

        // Act
        var result = tagIdJsonConverter.Read(ref jsonReader, typeof(long), new JsonSerializerOptions());

        // Assert
        Assert.IsType<TagId>(result);
        Assert.Equal(result, expected);
    }

    [Fact]
    public void Read_InvalidInt64_ReturnsNull()
    {
        // Arrange
        string json = "\"string\"";
        byte[] jsonUtf8Bytes = Encoding.UTF8.GetBytes(json);
        var jsonReader = new Utf8JsonReader(jsonUtf8Bytes);
        jsonReader.Read();

        var tagIdJsonConverter = new TagIdJsonConverter();

        // Act
        var result = tagIdJsonConverter.Read(ref jsonReader, typeof(long), new JsonSerializerOptions());

        // Act / Assert
        Assert.Null(result);
    }
}
