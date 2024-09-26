using MediatR;

namespace Basket.Application.Commads
{
    public class DeleteShoppingCartCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public DeleteShoppingCartCommand(string username)
        {
            UserName = username;

        }
    }
}
