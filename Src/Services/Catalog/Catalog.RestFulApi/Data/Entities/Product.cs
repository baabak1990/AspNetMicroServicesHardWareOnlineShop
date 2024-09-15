using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.RestFulApi.Data.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement(nameof(Name))]
        public string Name { get; set; }
        [BsonElement(nameof(Catagory))]
        public string Catagory { get; set; }
        [BsonElement(nameof(Summery))]
        public string Summery { get; set; }
        [BsonElement(nameof(Price))]
        public decimal Price { get; set; }
    }
}
