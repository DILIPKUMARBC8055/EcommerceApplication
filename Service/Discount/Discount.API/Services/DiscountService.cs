using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using System.Runtime.InteropServices;

namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var cmd = new CreateDiscountCommand(request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount);
            var result = await _mediator.Send(cmd);
            return result;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var coupon = await _mediator.Send(query);
            return coupon;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cmd = new CreateDiscountCommand(request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount);
            return await _mediator.Send(cmd);
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var cmd = new DeleteDiscountCommand(request.ProductName);
            await _mediator.Send(cmd);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
