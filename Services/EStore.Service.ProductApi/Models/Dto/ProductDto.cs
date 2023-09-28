﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EStore.Service.ProductApi.Models.Dto
{
	public class ProductDto
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public string Description { get; set; }
		public string CategoryName { get; set; }
		public string ImageUrl { get; set; }
		[JsonPropertyName("quantity")]
		public int StockQuantity { get; set; }
		public int CategoryId { get; set; }
		public CategoryDto Category { get; set; }

	}
}
