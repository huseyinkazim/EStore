using EStore.Service.ShoppingCartApi.Models.Dto;

namespace EStore.Service.ShoppingCartApi.Service.Interface
{
	public interface ICouponService
	{
		Task<CouponDto> GetCoupon(string couponCode);
	}
}
