using AutoMapper;
using EStore.Service.CouponApi.Models;
using EStore.Service.CouponApi.Models.Dto;

namespace EStore.Service.CouponApi.Mapping
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CouponDto, Coupon>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}
