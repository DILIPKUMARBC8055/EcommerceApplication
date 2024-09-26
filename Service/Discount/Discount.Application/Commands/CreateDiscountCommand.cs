using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public class CreateDiscountCommand : IRequest<CouponModel>
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Amount { get; set; }
        public CreateDiscountCommand(string productName, string productDescription, decimal amount)

        {
            ProductName = productName;
            ProductDescription = productDescription;
            Amount = amount;

        }
        public CreateDiscountCommand(CreateDiscountCommand obj)
        {
            ProductName = obj.ProductName;
            ProductDescription = obj.ProductDescription;
            Amount = obj.Amount;


        }
    }
}
