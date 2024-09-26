using Catalog.Core.Entities;

namespace Catalog.Core.Repositary
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> getProducts();
        Task<Product> getProductsById(string id);
        Task<IEnumerable<Product>> getProductsByBrand(string brand);
        Task<IEnumerable<Product>> getProductsByType(string type);
        Task<IEnumerable<Product>> getProductsByName(string name);

        Task<Product> createProduct(Product product);
        Task<bool> updateProduct(Product product);
        Task<bool> deleteProduct(string id);


    }
}
