using Catalog.RestFulApi.Data.Entities;
using MongoDB.Driver;

namespace Catalog.RestFulApi.Data.Context;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}