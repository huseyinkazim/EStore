﻿using System.ComponentModel.DataAnnotations;

namespace EStore_ProductService.Model
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public ICollection<Product> Products { get; set; }
	}

}
