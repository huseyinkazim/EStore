using EStore.Service.AuthApi.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EStore.Service.AuthApi.Models;
using EStore.Service.AuthApi.IServices;
using EStore.Service.AuthApi.Services;
using EStore.Service.AuthApi;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowLocalhost", r =>
	{
		r
			//.WithOrigins("http://localhost:4200")
			.WithOrigins(builder.Configuration["AllowedHosts"])

			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("myDb1"));
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

var app = builder.Build();
app.Use(async (context, next) =>
{
	var currentEndpoint = context.GetEndpoint();

	if (currentEndpoint is null)
	{
		await next(context);
		return;
	}

	Console.WriteLine($"Endpoint: {currentEndpoint.DisplayName}");

	if (currentEndpoint is RouteEndpoint routeEndpoint)
	{
		Console.WriteLine($"  - Route Pattern: {routeEndpoint.RoutePattern}");
	}

	foreach (var endpointMetadata in currentEndpoint.Metadata)
	{
		Console.WriteLine($"  - Metadata: {endpointMetadata}");
	}

	await next(context);
});
app.UseRouting();

app.UseCors("AllowLocalhost");
app.UseMiddleware<CorsErrorLoggerMiddleware>();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
SeedProcess();

app.Run();

void SeedProcess()
{
	using (var scope = app.Services.CreateScope())
	{
		var serviceProvider = scope.ServiceProvider;
		SeedData.InitializeAsync(serviceProvider).Wait();
	}
}

void ApplyMigration()
{
	using (var scope = app.Services.CreateScope())
	{
		var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		if (_db.Database.GetPendingMigrations().Count() > 0)
		{
			_db.Database.Migrate();
		}
	}
}