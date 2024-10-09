using AutoMapper;
using Discount.Application.Queries;
using Discount.Core.Repositary;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly ICouponRepositary _couponRepositary;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDiscountQueryHandler> _logger;

        public GetDiscountQueryHandler(ICouponRepositary couponRepositary, IMapper mapper, ILogger<GetDiscountQueryHandler> logger
            )
        {
            _couponRepositary = couponRepositary;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepositary.GetCoupon(request.ProductName);
            if (coupon == null)
            {
                return null;
            }
            var couponModel = new CouponModel { Amount = (int)coupon.Amount, Description = coupon.ProductDescription, Id = coupon.Id, ProductName = coupon.ProductName };
            _logger.LogInformation($"The Discount coupon for {coupon.ProductName} send success");
            return couponModel;
        }
    }
}
