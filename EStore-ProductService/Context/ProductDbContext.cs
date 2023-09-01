using EStore_ProductService.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EStore_ProductService.Context
{
	public class ProductDbContext : DbContext
	{
		public ProductDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductReview> ProductReviews { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<ProductTag> ProductTags { get; set; }
	}



}
