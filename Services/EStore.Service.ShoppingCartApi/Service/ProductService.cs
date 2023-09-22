using EStore.Service.ShoppingCartApi.Models.Dto;
using EStore.Service.ShoppingCartApi.Service.Interface;
using Newtonsoft.Json;

namespace EStore.Service.ShoppingCartApi.Service
{
	public class ProductService : IProductService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductService(IHttpClientFactory clientFactory)
		{
			_httpClientFactory = clientFactory;
		}
		public async Task<IEnumerable<ProductDto>> GetProducts(IEnumerable<int> ids)
		{
			var client = _httpClientFactory.CreateClient("ProductAPI");
			var response = await client.PostAsJsonAsync("api/Product/GetByIds", ids);
			var apiContet = await response.Content.ReadAsStringAsync();
			var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
			if (resp != null && resp.IsSuccess)
			{
				return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
			}
			return new List<ProductDto>();
		}
	}
}
