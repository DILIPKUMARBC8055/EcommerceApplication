using Catalog.Core.Entities;
using Catalog.Core.Repositary;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositary
{
    public class TypesRepositary : IProductTypesRepo
    {
        private IRepositaryContext context;
        public TypesRepositary(IRepositaryContext context)
        {
            this.context = context;

        }
        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            return await context.Types.Find(a => true).ToListAsync();
        }
    }
}
