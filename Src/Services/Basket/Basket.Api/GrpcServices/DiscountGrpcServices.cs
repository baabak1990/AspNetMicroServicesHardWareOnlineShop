using DiscountGrpc.Protos;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcServices
    {
        private readonly DiscountProtoServices.DiscountProtoServicesClient _discountProtoServices;

        public DiscountGrpcServices(DiscountProtoServices.DiscountProtoServicesClient discountProtoServices)
        {
            _discountProtoServices = discountProtoServices;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDuscountRequest() { ProductName = productName };
            return await _discountProtoServices.GetDiscountAsync(discountRequest);
        }
    }
}
