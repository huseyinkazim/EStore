﻿using AutoMapper;
using EStore.Service.ProductApi.Models;
using EStore.Service.ProductApi.Models.Dto;

namespace EStore.Service.ProductApi.Extension
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<ProductDto, Product>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}