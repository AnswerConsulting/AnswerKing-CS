﻿using Answer.King.Domain.Inventory.Models;

namespace Answer.King.Domain.Inventory;

// Todo: look at custom deserialisation: https://stackoverflow.com/questions/42336751/custom-deserialization
public class Category : IAggregateRoot
{
    public Category(string name, string description)
    {
        Guard.AgainstNullOrEmptyArgument(nameof(name), name);
        Guard.AgainstNullOrEmptyArgument(nameof(description), description);

        this.Id = 0;
        this.Name = name;
        this.Description = description;
        this.LastUpdated = this.CreatedOn = DateTime.UtcNow;
        this._Products = new List<ProductId>();
        this.Retired = false;
    }

    // ReSharper disable once UnusedMember.Local
    private Category(
        long id,
        string name,
        string description,
        DateTime createdOn,
        DateTime lastUpdated,
        IList<ProductId>? products,
        bool retired)
    {
        Guard.AgainstDefaultValue(nameof(id), id);
        Guard.AgainstNullOrEmptyArgument(nameof(name), name);
        Guard.AgainstNullOrEmptyArgument(nameof(description), description);
        Guard.AgainstDefaultValue(nameof(createdOn), createdOn);
        Guard.AgainstDefaultValue(nameof(lastUpdated), lastUpdated);

        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.CreatedOn = createdOn;
        this.LastUpdated = lastUpdated;
        this._Products = products ?? new List<ProductId>();
        this.Retired = retired;
    }


    public long Id { get; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTime CreatedOn { get; }

    public DateTime LastUpdated { get; private set; }

    private IList<ProductId> _Products { get; }

    public IReadOnlyCollection<ProductId> Products => (this._Products as List<ProductId>)!;

    public bool Retired { get; private set; }

    public void Rename(string name, string description)
    {
        Guard.AgainstNullOrEmptyArgument(nameof(name), name);
        Guard.AgainstNullOrEmptyArgument(nameof(description), description);

        this.Name = name;
        this.Description = description;
        this.LastUpdated = DateTime.UtcNow;
    }

    public void AddProduct(ProductId productId)
    {
        if (this.Retired)
        {
            throw new CategoryLifecycleException("Cannot add product to retired catgory.");
        }

        var exists = this._Products.Any(p => p.Id == productId.Id);

        if (exists)
        {
            return;
        }

        this._Products.Add(productId);

        this.LastUpdated = DateTime.UtcNow;
    }

    public void RemoveProduct(ProductId productId)
    {
        if (this.Retired)
        {
            throw new CategoryLifecycleException("Cannot remove product from retired catgory.");
        }

        var existing = this._Products.SingleOrDefault(p => p.Id == productId.Id);

        if (existing == null)
        {
            return;
        }

        this._Products.Remove(existing);

        this.LastUpdated = DateTime.UtcNow;
    }

    public void RetireCategory()
    {
        if (this.Retired)
        {
            return;
        }

        if (this._Products.Any())
        {
            throw new CategoryLifecycleException("Cannot retire category whilst there are still products assigned.");
        }

        this.Retired = true;

        this.LastUpdated = DateTime.UtcNow;
    }
}

[Serializable]
public class CategoryLifecycleException : Exception
{
    public CategoryLifecycleException(string message) : base(message)
    {
    }

    public CategoryLifecycleException() : base()
    {
    }

    public CategoryLifecycleException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
