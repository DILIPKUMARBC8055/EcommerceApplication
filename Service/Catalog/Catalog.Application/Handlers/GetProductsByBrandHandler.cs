using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductsByBrandHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
    {
        private readonly IProductRepo _productRepo;

        public GetProductsByBrandHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }


        public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepo.getProductsByBrand(request.BrandName);
            var productRes = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productRes;
        }
    }
}
