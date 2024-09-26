
using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllBrandHandler : IRequestHandler<GetAllBrandQuery, IList<BrandResponse>>
    {
        private readonly IProductBrandsRepo _brandRepositary;

        public GetAllBrandHandler(IProductBrandsRepo brandsRepo)
        {
            _brandRepositary = brandsRepo;

        }

        public async Task<IList<BrandResponse>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepositary.getProductBrandsAsync();
            var response = ProductMapper.Mapper.Map<IList<BrandResponse>>(brands);
            return response;
        }
    }
}
