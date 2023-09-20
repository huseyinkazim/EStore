using AutoMapper;
using EStore.Service.ShoppingCartApi.Context;
using EStore.Service.ShoppingCartApi.Extension;
using EStore.Service.ShoppingCartApi.Service;
using EStore.Service.ShoppingCartApi.Service.Interface;
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

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ApiHttpClientHandler>();

builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IProductService, ProductService>();
var serviceUrls = builder.Configuration.GetSection("ServiceUrls");

builder.Services.AddHttpClient("ProductAPI", client =>
{
	client.BaseAddress = new Uri(serviceUrls.GetValue<string>("ProductAPI").ToString());
}).AddHttpMessageHandler<ApiHttpClientHandler>();
builder.Services.AddHttpClient("CouponAPI", client =>
{
	client.BaseAddress = new Uri(serviceUrls.GetValue<string>("CouponAPI").ToString());
}).AddHttpMessageHandler<ApiHttpClientHandler>(); ;


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


