using EStore.Service.AuthApi.Models.Dtos;

namespace EStore.Service.AuthApi.IServices
{
	public interface IAuthService
	{
		Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
		Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
	}
}
