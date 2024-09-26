using Catalog.Core.Entities;
using Catalog.Core.Repositary;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositary
{
    public class BrandRepositary : IProductBrandsRepo
    {
        private IRepositaryContext context;
        public BrandRepositary(IRepositaryContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ProductBrand>> getProductBrandsAsync()
        {
            return await context.Brands.Find(obj => true).ToListAsync();
        }
    }
}
