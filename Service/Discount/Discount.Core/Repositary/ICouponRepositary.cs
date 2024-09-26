using Discount.Core.Entities;

namespace Discount.Core.Repositary
{
    public interface ICouponRepositary
    {
        Task<Coupon> GetCoupon(string productName);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string productName);
    }
}
