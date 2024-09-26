using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepo _productRepo;

        public CreateProductCommandHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            var productCreated = await _productRepo.createProduct(productEntity);
            if (productCreated == null)
            {
                throw new ApplicationException("Failed to create the product");
            }
            return ProductMapper.Mapper.Map<ProductResponse>(productCreated);
        }
    }
}
