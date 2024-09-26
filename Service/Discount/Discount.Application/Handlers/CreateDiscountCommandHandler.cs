using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositary;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly ICouponRepositary _couponRepositary;
        private readonly IMapper _mapper;

        public CreateDiscountCommandHandler(ICouponRepositary couponRepositary, IMapper mapper)
        {
            _couponRepositary = couponRepositary;
            _mapper = mapper;
        }
        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var affected = await _couponRepositary.CreateCoupon(coupon);

            return _mapper.Map<CouponModel>(coupon);
        }
    }
}
