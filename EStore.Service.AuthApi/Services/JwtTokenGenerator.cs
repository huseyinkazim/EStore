using EStore.Service.AuthApi.IServices;
using EStore.Service.AuthApi.Models;

namespace EStore.Service.AuthApi.Services
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		public string GenerateToken(ApplicationUser applicationUser)
		{
			throw new NotImplementedException();
		}
	}
}
