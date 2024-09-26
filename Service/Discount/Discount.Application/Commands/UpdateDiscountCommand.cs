using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public class UpdateDiscountCommand : IRequest<bool>
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Amount { get; set; }
        public UpdateDiscountCommand(string productName, string productDescription, decimal amount)

        {
            ProductName = productName;
            ProductDescription = productDescription;
            Amount = amount;

        }
    }
}
