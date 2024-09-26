using Catalog.Core.Entities;

namespace Catalog.Core.Repositary
{
    public interface IProductBrandsRepo
    {
        Task<IEnumerable<ProductBrand>> getProductBrandsAsync();

    }
}
