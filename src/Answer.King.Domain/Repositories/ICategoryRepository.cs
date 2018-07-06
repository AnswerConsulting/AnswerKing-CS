﻿using System;
using System.Threading.Tasks;
using Answer.King.Domain.Inventory;

namespace Answer.King.Domain.Repositories
{
    public interface ICategoryRepository : IAggregateRepository<Category>
    {
        Task<Category> GetByProductId(Guid productId);
    }
}