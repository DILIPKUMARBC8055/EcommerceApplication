using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IList<ProductResponse>>
    {
        private readonly IProductRepo _context;


        public GetAllProductHandler(IProductRepo context)
        {
            _context = context;

        }
        public async Task<IList<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.getProducts();
            if (products == null)
            {
                return null;
            }
            var productDto = ProductMapper.Mapper.Map<IList<ProductResponse>>(products);
            return productDto;
        }
    }
}
