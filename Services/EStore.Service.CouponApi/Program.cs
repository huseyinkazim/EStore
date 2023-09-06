using AutoMapper;
using EStore.Service.CouponApi.Context;
using EStore.Service.CouponApi.Extensions;
using EStore.Service.CouponApi.Mapping;
using Microsoft.AspNetCore.Authorization;
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
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowLocalhost", r =>
	{
		r
			.WithOrigins("http://localhost:4200")
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});
builder.Services.AddCors();

builder.AddAppAuthetication();
builder.Services.AddAppAuthorization();

// DI'yi yapýlandýrýn
builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
var app = builder.Build();

app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
