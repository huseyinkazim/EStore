﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EStore.Service.ProductApi.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		[Required]
		public string Name { get; set; }
		[Range(1, 1000)]
		public double Price { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public int StockQuantity { get; set; }
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
	}
}
