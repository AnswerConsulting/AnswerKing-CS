using System.Linq;
using Answer.King.Domain.Repositories.Models;

namespace Answer.King.Infrastructure.SeedData;

public class ProductDataSeeder : ISeedData
{
    public void SeedData(ILiteDbConnectionFactory connections, int maxEntries)
    {
        var db = connections.GetConnection();
        var collection = db.GetCollection<Product>();

        if (DataSeeded)
        {
            return;
        }

        var none = collection.Count() < 1;
        if (none)
        {
            var products = ProductData.Products.Take(maxEntries);
            collection.Insert(products);
        }

        DataSeeded = true;
    }

    private static bool DataSeeded { get; set; }
}
