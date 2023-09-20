using EStore.Service.ShoppingCartApi.Models.Dto;

namespace EStore.Service.ShoppingCartApi.Service.Interface
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDto>> GetProducts(IEnumerable<int> ids);

	}
}
