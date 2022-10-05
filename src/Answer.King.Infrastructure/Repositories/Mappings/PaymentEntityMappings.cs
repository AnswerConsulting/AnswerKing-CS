﻿using LiteDB;

namespace Answer.King.Infrastructure.Repositories.Mappings;

public class PaymentEntityMappings : IEntityMapping
{
    public void RegisterMapping(BsonMapper mapper)
    {
        mapper.RegisterType
        (
            serialize: payment =>
            {
                var doc = new BsonDocument
                {
                    ["_id"] = payment.Id,
                    ["OrderId"] = payment.OrderId,
                    ["Amount"] = payment.Amount,
                    ["OrderTotal"] = payment.OrderTotal,
                    ["date"] = payment.Date
                };

                return doc;
            },
            deserialize: bson =>
            {
                var doc = bson.AsDocument;

                return PaymentFactory.CreatePayment(
                    doc["_id"].AsGuid,
                    doc["OrderId"].AsGuid,
                    doc["Amount"].AsDouble,
                    doc["OrderTotal"].AsDouble,
                    doc["date"].AsDateTime);
            }
        );
    }
}