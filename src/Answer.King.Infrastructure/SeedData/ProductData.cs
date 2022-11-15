﻿using System;
using System.Collections.Generic;
using System.Linq;
using Answer.King.Domain.Repositories.Models;
using Answer.King.Infrastructure.Repositories.Mappings;

namespace Answer.King.Infrastructure.SeedData;

internal static class ProductData
{
    public static IList<Product> Products { get; } = new List<Product>
    {
        ProductFactory.CreateProduct(
            1,
            "Fish",
            "Delicious and satisfying.",
            5.99,
            DateTime.UtcNow,
            DateTime.UtcNow,
            Categories(1),
            false),
        ProductFactory.CreateProduct(
            2,
            "Chips",
            "Nothing more to say.",
            2.99,
            DateTime.UtcNow,
            DateTime.UtcNow,
            Categories(2),
            false)
    };


    private static IList<Category> Categories(long id)
    {
        return CategoryData.Categories.Where(c => c.Id == id)
            .Select(x => new Category(x.Id, x.Name, x.Description)).ToList();
    }
}
