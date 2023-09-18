using AutoMapper;
using EStore.Service.ProductApi.Context;
using EStore.Service.ProductApi.Extension;
using EStore.Service.ProductApi.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("myDb1"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
var orgin = builder.Configuration["CorsAllowedOrigins"].Split(",");
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowLocalhost", r =>
	{
		r
			.WithOrigins(orgin)
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});
builder.AddAppAuthetication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors("AllowLocalhost");
app.Use((context, next) =>
{
	var cultureQuery = context.Request.Query["culture"];
	if (!string.IsNullOrWhiteSpace(cultureQuery))
	{
		var culture = new CultureInfo(cultureQuery);
		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;
	}

	// Call the next delegate/middleware in the pipeline
	return next();
});
// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
