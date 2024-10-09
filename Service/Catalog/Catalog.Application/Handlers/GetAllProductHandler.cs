using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, List<ProductResponse>>
    {
        private readonly IProductRepo _context;


        public GetAllProductHandler(IProductRepo context)
        {
            _context = context;

        }
        public async Task<List<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.getProducts();
            if (products == null)
            {
                return null;
            }
            var productDto = ProductMapper.Mapper.Map<List<ProductResponse>>(products);
            return productDto;
        }
    }
}
