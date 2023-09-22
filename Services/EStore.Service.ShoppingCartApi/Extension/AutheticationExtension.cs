﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EStore.Service.ShoppingCartApi.Extension
{
	public static class AutheticationExtension
	{

		public static void AddAppAuthetication(this WebApplicationBuilder builder)
		{
			var settingsSection = builder.Configuration.GetSection("ApiSettings");

			var secret = settingsSection.GetValue<string>("Secret");
			var issuer = settingsSection.GetValue<string>("Issuer");
			var audience = settingsSection.GetValue<string>("Audience");

			var key = Encoding.ASCII.GetBytes(secret);
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidIssuer = issuer,
					ValidAudience = audience,
					ValidateAudience = true
				};
			});
		}
	}
}
