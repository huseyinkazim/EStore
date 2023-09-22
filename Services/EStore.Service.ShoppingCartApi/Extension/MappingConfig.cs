using AutoMapper;
using EStore.Service.ShoppingCartApi.Models;
using EStore.Service.ShoppingCartApi.Models.Dto;

namespace EStore.Service.ShoppingCartApi.Extension
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
				config.CreateMap<CartDetailDto, CartDetail>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}
