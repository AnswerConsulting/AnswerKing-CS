﻿using System;
using System.Collections.Generic;
using System.Linq;
using Answer.King.Domain.Orders;
using Answer.King.Domain.Orders.Models;
using Answer.King.Infrastructure.Repositories.Mappings;

namespace Answer.King.Infrastructure.SeedData;

internal static class OrderData
{
    public static IList<Order> Orders { get; } = GetOrders();

    private static IList<Order> GetOrders()
    {
        return new List<Order>
        {
            new Order(),
            OrderWithLineItems(),
            CancelledOrder(),
        };
    }

    private static Order OrderWithLineItems()
    {
        var fish = ProductData.Products.SingleOrDefault(p => p.Id == 1);

        var fishCategories = CategoryData.Categories
            .Where(c => fish!.Categories.Select(cs => cs.Value).Contains(c.Id))
            .Select(x => new Category(x.Id, x.Name, x.Description))
            .ToList();

        var fishTags = TagData.Tags
            .Where(c => fish!.Tags.Select(cs => cs.Value).Contains(c.Id))
            .Select(x => new Tag(x.Id, x.Name, x.Description))
            .ToList();

        var fishOrder = new Product(fish!.Id, fish.Name, fish.Description, fish.Price, fishCategories, fishTags);

        var lineItem1 = new LineItem(fishOrder);
        lineItem1.AddQuantity(1);

        var chips = ProductData.Products.SingleOrDefault(p => p.Id == 2);

        var chipsCategories = CategoryData.Categories
            .Where(c => chips!.Categories.Select(cs => cs.Value).Contains(c.Id))
            .Select(x => new Category(x.Id, x.Name, x.Description))
            .ToList();

        var chipsTags = TagData.Tags
            .Where(c => chips!.Tags.Select(cs => cs.Value).Contains(c.Id))
            .Select(x => new Tag(x.Id, x.Name, x.Description))
            .ToList();

        var chipsOrder = new Product(chips!.Id, chips.Name, chips.Description, chips.Price, chipsCategories, chipsTags);

        var lineItem2 = new LineItem(chipsOrder);
        lineItem2.AddQuantity(2);

        var lineItems = new List<LineItem>
        {
            lineItem1,
            lineItem2
        };

        return OrderFactory.CreateOrder(
            0,
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow.AddMinutes(-10),
            OrderStatus.Created,
            lineItems
        );
    }

    private static Order CancelledOrder()
    {
        var lineItems = new List<LineItem>();

        return OrderFactory.CreateOrder(
            0,
            DateTime.UtcNow.AddHours(-3),
            DateTime.UtcNow.AddMinutes(-50),
            OrderStatus.Cancelled,
            lineItems
        );
    }
}
