using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByTypeQueryHandler : IRequestHandler<GetProductByTypeQuery, IList<ProductResponse>>
    {
        private readonly IProductRepo _productRepositay;

        public GetProductByTypeQueryHandler(IProductRepo productRepositay)
        {
            _productRepositay = productRepositay;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByTypeQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepositay.getProductsByType(request.Type);
            var productRes = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productRes;
        }
    }
}
