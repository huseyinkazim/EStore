using EStore.Service.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Service.ProductApi.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 1,
				CategoryName = "Appetizer",
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 2,
				CategoryName = "Dessert",
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 3,
				CategoryName = "Entree",
			});

			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 1,
				Name = "Samosa",
				Price = 15,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/14.jpg",
				CategoryId = 1,
				StockQuantity = 10
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 2,
				Name = "Paneer Tikka",
				Price = 13.99,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/12.jpg",
				CategoryId = 2,
				StockQuantity = 10
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 3,
				Name = "Sweet Pie",
				Price = 10.99,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/11.jpg",
				CategoryId = 2,
				StockQuantity = 10
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 4,
				Name = "Pav Bhaji",
				Price = 15,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/13.jpg",
				CategoryId = 3,
				StockQuantity = 10
			});

		}
	}
}
