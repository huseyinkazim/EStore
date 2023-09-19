using EStore.Service.ShoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Service.ShoppingCartApi.Context
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions options):base(options)
		{
				
		}
		public DbSet<CartHeader> CartHeaders { get; set; }
		public DbSet<CartDetail> CartDetails { get; set; }
	}
}
