using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepo _productRepo;

        public GetProductByNameQueryHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepo.getProductsByName(request.Name);
            var producRes = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return producRes;

        }
    }
}
