using AutoMapper;
using EStore.Service.CouponApi.Context;
using EStore.Service.CouponApi.Mapping;
using Microsoft.EntityFrameworkCore;
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
	options.AddPolicy("AllowLocalhost", builder =>
	{
		builder.WithOrigins("http://localhost:4200")
			   .AllowAnyHeader()
			   .AllowAnyMethod();
	});
});
var app = builder.Build();
app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
