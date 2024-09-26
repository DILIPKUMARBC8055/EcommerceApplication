using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepo _context;

        public GetProductByIdHandler(IProductRepo context)
        {
            _context = context;
        }
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.getProductsById(request.Id);
            var prouctRes = ProductMapper.Mapper.Map<ProductResponse>(product);
            return prouctRes;
        }
    }
}
