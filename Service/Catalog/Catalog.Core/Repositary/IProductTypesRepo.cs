using Catalog.Core.Entities;

namespace Catalog.Core.Repositary
{
    public interface IProductTypesRepo
    {
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
    }
}
