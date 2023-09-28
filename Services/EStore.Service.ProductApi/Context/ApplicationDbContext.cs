using EStore.Service.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Service.ProductApi.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}
		public ApplicationDbContext(string connString)
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
				CategoryName = "Yemek",
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 2,
				CategoryName = "Elektronik",
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 3,
				CategoryName = "Ana Yemek",
				BaseCategoryId = 1
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 6,
				CategoryName = "Tatlı",
				BaseCategoryId = 1
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 7,
				CategoryName = "Aparatif",
				BaseCategoryId = 1
			});

			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 4,
				CategoryName = "Telefon",
				BaseCategoryId = 2
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 5,
				CategoryName = "Android Telefon",
				BaseCategoryId = 4
			});
			modelBuilder.Entity<Category>().HasData(new Category
			{
				Id = 8,
				CategoryName = "Temizlik",
			});


			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 1,
				Name = "Samosa",
				Price = 15,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "assets/images/14.jpg",
				CategoryId = 7,
				StockQuantity = 10
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 2,
				Name = "Paneer Tikka",
				Price = 13.99,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "assets/images/12.jpg",
				CategoryId = 7,
				StockQuantity = 10
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 3,
				Name = "Sweet Pie",
				Price = 10.99,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "assets/images/11.jpg",
				CategoryId = 6,
				StockQuantity = 10
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 4,
				Name = "Pav Bhaji",
				Price = 15,
				Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "assets/images/13.jpg",
				CategoryId = 3,
				StockQuantity = 10
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 5,
				Name = "Solo Bambu Katkılı 40'lı Tuvalet Kağıdı",
				Price = 19,
				Description = "Çevre dostu tuvalet kağıdı.",
				ImageUrl = "assets/images/Tuvalet Kağıdı.webp",
				CategoryId = 8,
				StockQuantity = 10,
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 6,
				Name = "Xiaomi Redmi Note 12 Pro",
				Price = 1900,
				Description = "Xiaomi Redmi Note 12 Pro 8 GB 256 GB (Xiaomi Türkiye Garantili)",
				ImageUrl = "assets/images/Redmi Note 12 Pro.webp",
				CategoryId = 5,
				StockQuantity = 10,
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 7,
				Name = "Samsung Galaxy A04E",
				Price = 1700,
				Description = "Samsung Galaxy A04E 4 GB 128 GB (Samsung Türkiye Garantili)",
				ImageUrl = "assets/images/Samsung Galaxy A04E.jpg",
				CategoryId = 5,
				StockQuantity = 10,
			});
		}
	}
}
