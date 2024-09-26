using AutoMapper;
using Discount.Application.Queries;
using Discount.Core.Repositary;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly ICouponRepositary _couponRepositary;
        private readonly IMapper _mapper;

        public GetDiscountQueryHandler(ICouponRepositary couponRepositary, IMapper mapper
            )
        {
            _couponRepositary = couponRepositary;
            _mapper = mapper;
        }

        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepositary.GetCoupon(request.ProductName);
            if (coupon == null)
            {
                return null;
            }
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
