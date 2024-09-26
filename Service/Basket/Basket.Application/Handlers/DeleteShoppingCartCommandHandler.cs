using Basket.Application.Commads;
using Basket.Core.Repositary;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, bool>
    {
        private readonly IBasketRepositary _basketRepositary;

        public DeleteShoppingCartCommandHandler(IBasketRepositary basketRepositary)
        {
            _basketRepositary = basketRepositary;
        }
        public async Task<bool> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var output = await _basketRepositary.DeleteShoppingCartAsyn(request.UserName);
            return output;
        }
    }
}
