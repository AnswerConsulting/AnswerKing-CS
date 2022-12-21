﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Answer.King.Domain.Orders;
using Answer.King.Domain.Orders.Models;

namespace Answer.King.Infrastructure.Repositories.Mappings;

internal static class OrderFactory
{
    private static ConstructorInfo? OrderConstructor { get; } = typeof(Order)
            .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
            .SingleOrDefault(c => c.IsPrivate && c.GetParameters().Length > 0);

    public static Order CreateOrder(
        long id,
        DateTime createdOn,
        DateTime lastUpdated,
        OrderStatus status,
        IList<LineItem> lineItems)
    {
        var parameters = new object[] { id, createdOn, lastUpdated, status, lineItems };

        /* invoking a private constructor will wrap up any exception into a
         * TargetInvocationException so here I unwrap it
         */
        try
        {
            return (Order)OrderConstructor?.Invoke(parameters)!;
        }
        catch (TargetInvocationException ex)
        {
            var exception = ex.InnerException ?? ex;
            throw exception;
        }
    }
}
