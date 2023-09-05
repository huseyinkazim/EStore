using AutoMapper;
using EStore.Service.CouponApi.Context;
using EStore.Service.CouponApi.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("myDb1"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddCors(options =>
//{
//	options.AddPolicy("AllowLocalhost", r =>
//	{
//		r
//			.WithOrigins("*")
//			//.AllowAnyOrigin()
//			.AllowAnyHeader()
//			.AllowAnyMethod();
//	});
//});
builder.Services.AddCors();

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

//app.UseCors("AllowLocalhost");
app.UseCors(x => x.AllowAnyMethod()
				  .AllowAnyHeader()
				  .SetIsOriginAllowed(origin => true) // allow any origin
				  .AllowCredentials());
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
