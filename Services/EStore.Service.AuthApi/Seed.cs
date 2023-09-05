namespace EStore.Service.AuthApi
{
	using EStore.Service.AuthApi.Models;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Threading.Tasks;

	public static class SeedData
	{
		public static async Task InitializeAsync(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			// Rolleri oluştur
			if (!await roleManager.RoleExistsAsync("admin"))
			{
				await roleManager.CreateAsync(new IdentityRole("admin"));
			}

			// Admin kullanıcısını oluştur
			if (await userManager.FindByNameAsync("admin") == null)
			{
				var adminUser = new ApplicationUser
				{
					UserName = "admin",
					Email = "admin@example.com" // Admin e-posta adresini burada belirleyebilirsiniz
				};

				var result = await userManager.CreateAsync(adminUser, "Qwert!2345"); // Admin şifresini burada belirleyebilirsiniz

				if (result.Succeeded)
				{
					// Admin kullanıcısına admin rolünü ata
					await userManager.AddToRoleAsync(adminUser, "admin");
				}
			}
		}
	}

}
