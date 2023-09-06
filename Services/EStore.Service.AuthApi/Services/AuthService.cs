﻿using EStore.Service.AuthApi.Models.Dtos;
using EStore.Service.AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using EStore.Service.AuthApi.Context;
using EStore.Service.AuthApi.IServices;

namespace EStore.Service.AuthApi.Services
{
	public class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;

		public AuthService(ApplicationDbContext db,
			UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<bool> AssignRole(string email, string roleName)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
			if (user != null)
			{
				if (!_roleManager.RoleExistsAsync(roleName).Result)
				{
					await _roleManager.CreateAsync(new IdentityRole(roleName));
				}
				await _userManager.AddToRoleAsync(user, roleName);
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

			bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

			if (user == null || isValid == false)
			{
				return new LoginResponseDto() { User = null, Token = "" };
			}
			var roles = await _userManager.GetRolesAsync(user);
			//if user was found , Generate JWT Token
			var token = _jwtTokenGenerator.GenerateToken(user, roles);

			UserDto userDTO = new()
			{
				Email = user.Email,
				ID = user.Id,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber
			};

			LoginResponseDto loginResponseDto = new LoginResponseDto()
			{
				User = userDTO,
				Token = token
			};

			return loginResponseDto;
		}

		public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
		{
			ApplicationUser user = new()
			{
				UserName = registrationRequestDto.Email,
				Email = registrationRequestDto.Email,
				NormalizedEmail = registrationRequestDto.Email.ToUpper(),
				Name = registrationRequestDto.Name,
				PhoneNumber = registrationRequestDto.PhoneNumber
			};

			try
			{
				var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
				if (result.Succeeded)
				{
					var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

					UserDto userDto = new()
					{
						Email = userToReturn.Email,
						ID = userToReturn.Id,
						Name = userToReturn.Name,
						PhoneNumber = userToReturn.PhoneNumber
					};
					await AssignRole(userDto.Email, "user");
					return userDto;
				}
				else
				{
				}
			}
			catch (Exception ex)
			{
			}
			return new UserDto();
		}
	}
}