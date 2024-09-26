using Catalog.Core.Entities;
using Catalog.Core.Repositary;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositary
{
    public class ProductRepositary : IProductRepo
    {
        private IRepositaryContext context;
        public ProductRepositary(IRepositaryContext context)
        {
            this.context = context;

        }
        public async Task<Product> createProduct(Product product)
        {
            await context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> deleteProduct(string id)
        {
            var deltedProduct = await context.Products.DeleteOneAsync(p => p.Id == id);
            return deltedProduct.IsAcknowledged && deltedProduct.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> getProducts()
        {
            return await context.Products.Find(a => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> getProductsByBrand(string brand)
        {
            return await context.Products.Find<Product>(a => a.Brands.Name.ToLower() == brand.ToLower()).ToListAsync();
        }

        public async Task<Product> getProductsById(string id)
        {
            return await context.Products.Find<Product>(a => a.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Product>> getProductsByName(string name)
        {
            return await context.Products.Find<Product>(a => a.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<Product>> getProductsByType(string type)
        {
            return await context.Products.Find<Product>(obj => obj.Types.Name.ToLower() == type.ToLower()).ToListAsync();
        }

        public async Task<bool> updateProduct(Product product)
        {
            var updatedProduct = await context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
    }
}
