using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
    {
        private readonly IProductTypesRepo _context;

        public GetAllTypesHandler(IProductTypesRepo context)
        {
            _context = context;
        }
        public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _context.GetProductTypesAsync();
            var typeResponse = ProductMapper.Mapper.Map<IList<TypesResponse>>(types);
            return typeResponse;
        }
    }
}
