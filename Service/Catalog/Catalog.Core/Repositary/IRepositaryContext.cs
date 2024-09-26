using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Core.Repositary
{
    public interface IRepositaryContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<ProductBrand> Brands { get; }
        IMongoCollection<ProductType> Types { get; }
    }
}
