using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core.Entities;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepo _productRepositary;

        public UpdateProductCommandHandler(IProductRepo productRepositary)
        {
            _productRepositary = productRepositary;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ProductMapper.Mapper.Map<Product>(request);
            if (product == null)
            {
                throw new ApplicationException("There was some error in updateing Product");
            }
            var updates = await _productRepositary.updateProduct(product);
            return updates;
        }
    }
}
