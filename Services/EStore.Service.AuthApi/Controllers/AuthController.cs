using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EStore.Service.AuthApi.IServices;
using EStore.Service.AuthApi.Models.Dtos;
using Microsoft.AspNetCore.Cors;

namespace EStore.Service.AuthApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		protected ResponseDto _response;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
			_response = new();
		}
		[HttpGet("test")]
		public string test()
		{
			return "test";
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
		{
			var userDto = await _authService.Register(model);
			if (string.IsNullOrEmpty(userDto.ID))//todo:kullanımı test edilecek
			{
				_response.IsSuccess = false;
				_response.Message = "Kayıt ederken hata oluştu!";
				return BadRequest(_response);
			}
			return Ok(_response);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
		{
			var loginResponse = await _authService.Login(model);
			if (loginResponse.User == null)
			{
				_response.IsSuccess = false;
				_response.Message = "Username or password is incorrect";
				return BadRequest(_response);
			}
			_response.Result = loginResponse;
			return Ok(_response);
		}

		[HttpPost("AssignRole")]
		public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto model)
		{
			var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
			if (!assignRoleSuccessful)
			{
				_response.IsSuccess = false;
				_response.Message = "Error encountered";
				return BadRequest(_response);
			}
			return Ok(_response);
		}
	}
}
