using System.Linq;
using Answer.King.Domain.Orders;

namespace Answer.King.Infrastructure.SeedData;

public class OrderDataSeeder : ISeedData
{
    public void SeedData(ILiteDbConnectionFactory connections, int maxEntries)
    {
        var db = connections.GetConnection();
        var collection = db.GetCollection<Order>();

        if (DataSeeded)
        {
            return;
        }

        var none = collection.Count() < 1;
        if (none)
        {
            var orders = OrderData.Orders.Take(maxEntries);
            collection.Insert(orders);
        }

        DataSeeded = true;
    }

    private static bool DataSeeded { get; set; }
}
