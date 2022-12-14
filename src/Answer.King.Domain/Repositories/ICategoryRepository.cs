using Answer.King.Domain.Inventory;

namespace Answer.King.Domain.Repositories;

public interface ICategoryRepository : IAggregateRepository<Category>
{
    Task<IEnumerable<Category>> GetByProductId(long productId);

    Task<IEnumerable<Category>> GetByProductId(params long[] productIds);
}
