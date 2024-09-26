using Catalog.Application.Commands;
using Catalog.Core.Repositary;
using MediatR;

namespace Catalog.Application.Queries
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepo _productRepositary;

        public DeleteProductCommandHandler(IProductRepo productRepositary)
        {
            _productRepositary = productRepositary;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepositary.deleteProduct(request.Id);
        }
    }
}
