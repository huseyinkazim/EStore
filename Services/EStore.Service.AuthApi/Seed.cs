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

			if (!await roleManager.RoleExistsAsync("admin"))
			{
				await roleManager.CreateAsync(new IdentityRole("admin"));
			}
			if (!await roleManager.RoleExistsAsync("user"))
			{
				await roleManager.CreateAsync(new IdentityRole("user"));
			}
			if (await userManager.FindByNameAsync("admin") == null)
			{
				var adminUser = new ApplicationUser
				{
					UserName = "admin",
					Email = "admin@example.com" 
				};

				var result = await userManager.CreateAsync(adminUser, "Qwert!2345"); 

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, "admin");
				}
			}
			if (await userManager.FindByNameAsync("user") == null)
			{
				var user = new ApplicationUser
				{
					UserName = "user",
					Email = "user@example.com"
				};

				var result = await userManager.CreateAsync(user, "Qwert!2345"); 

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "user");
				}
			}
		}
	}

}
