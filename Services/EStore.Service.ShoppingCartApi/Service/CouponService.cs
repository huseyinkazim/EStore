using EStore.Service.ShoppingCartApi.Models.Dto;
using EStore.Service.ShoppingCartApi.Service.Interface;
using Newtonsoft.Json;

namespace EStore.Service.ShoppingCartApi.Service
{
	public class CouponService : ICouponService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public CouponService(IHttpClientFactory clientFactory)
		{
			_httpClientFactory = clientFactory;
		}
		public async Task<CouponDto> GetCoupon(string couponCode)
		{
			var client = _httpClientFactory.CreateClient("CouponAPI");
			var response = await client.GetAsync($"/api/Coupon/GetByCode/{couponCode}");
			var apiContet = await response.Content.ReadAsStringAsync();
			var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
			if (resp != null && resp.IsSuccess)
			{
				return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
			}
			return new CouponDto();

		}
	}
}
