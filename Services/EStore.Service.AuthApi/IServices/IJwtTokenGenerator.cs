using EStore.Service.AuthApi.Models;

namespace EStore.Service.AuthApi.IServices
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
	}
}
