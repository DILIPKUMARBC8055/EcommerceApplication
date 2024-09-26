using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers
{
    public class CouponMappingProfile : Profile
    {
        public CouponMappingProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
            CreateMap<Coupon, CreateDiscountCommand>().ReverseMap();
        }
    }
}
