﻿using Answer.King.Domain;

namespace Answer.King.Api.IntegrationTests.Common.Models;

public class Product
{
    public Product(long id, string name, string description, double price, IList<Category>? categories, bool retired)
    {
        Guard.AgainstDefaultValue(nameof(id), id);
        Guard.AgainstNullOrEmptyArgument(nameof(name), name);
        Guard.AgainstNullOrEmptyArgument(nameof(description), description);
        Guard.AgainstNegativeValue(nameof(price), price);
        //Guard.AgainstNullArgument(nameof(categories), categories);

        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Categories = categories ?? new List<Category>();
        this.Retired = retired;
    }
    public long Id { get; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public IList<Category>? Categories { get; set; }

    public bool Retired { get; private set; }

    public void Retire()
    {
        this.Retired = true;
    }
}
