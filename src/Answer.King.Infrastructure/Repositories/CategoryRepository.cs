﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Answer.King.Domain.Inventory;
using Answer.King.Domain.Repositories;
using Answer.King.Infrastructure.SeedData;
using LiteDB;

namespace Answer.King.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(ILiteDbConnectionFactory connections)
        {
            var db = connections.GetConnection();

            this.Collection = db.GetCollection<Category>();
            this.Collection.EnsureIndex("Products.Id");

            this.SeedData();
        }

        public LiteCollection<Category> Collection { get; }


        public Task<IEnumerable<Category>> Get()
        {
            return Task.FromResult(this.Collection.FindAll());
        }

        public Task<Category> Get(Guid id)
        {
            return Task.FromResult(this.Collection.FindOne(c => c.Id == id));
        }

        public Task Save(Category item)
        {
            return Task.FromResult(this.Collection.Upsert(item));
        }

        public Task<Category> GetByProductId(Guid productId)
        {
            return Task.FromResult(
                this.Collection.FindOne(c => c.Products.Any(p => p.Id == productId)));
        }

        private void SeedData()
        {
            if (DataSeeded)
            {
                return;
            }

            var none = this.Collection.Count() <= 0;
            if (none)
            {
                this.Collection.InsertBulk(CategoryData.Categories);
            }

            DataSeeded = true;
        }

        private static bool DataSeeded { get; set; }
    }
}