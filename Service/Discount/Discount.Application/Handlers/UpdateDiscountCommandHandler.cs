using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositary;
using MediatR;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, bool>
    {
        private readonly ICouponRepositary _couponRepositary;
        private readonly IMapper _mapper;
        public UpdateDiscountCommandHandler(ICouponRepositary couponRepositary, IMapper mapper)
        {
            _couponRepositary = couponRepositary;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var affected = await _couponRepositary.UpdateCoupon(coupon);

            return affected;
        }
    }
}
