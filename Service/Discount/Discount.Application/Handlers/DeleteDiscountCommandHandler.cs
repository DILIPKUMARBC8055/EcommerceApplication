using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Repositary;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly ICouponRepositary _couponRepositary;


        public DeleteDiscountCommandHandler(ICouponRepositary couponRepositary)
        {
            _couponRepositary = couponRepositary;

        }
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var result = await _couponRepositary.DeleteCoupon(request.ProductName);
            return result;
        }
    }
}
