using EStore.Service.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Service.CouponApi.Context
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Coupon> Coupons { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Coupon>().HasData(new Coupon
			{
				CouponId = 1,
				CouponCode = "10OFF",
				DiscountAmount = 10,
				MinAmount = 20
			});

		}
	}
}
