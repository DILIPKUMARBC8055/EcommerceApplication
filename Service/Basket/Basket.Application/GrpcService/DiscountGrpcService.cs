using Discount.Grpc.Protos;

namespace Basket.Application.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }
        public async Task<CouponModel> getDiscount(string productName)
        {
            var query = new GetDiscountRequest { ProductName = productName };
            try
            {
                return await _discountProtoServiceClient.GetDiscountAsync(query);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
