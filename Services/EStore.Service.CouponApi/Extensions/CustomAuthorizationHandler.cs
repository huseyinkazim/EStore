using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace EStore.Service.CouponApi.Extensions
{
	public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
		{
			if (context.User.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Name && claim.Value == "huseyinkazim@hotmail.com"))
			{
				context.Succeed(requirement);
			}
			else
			{
				// Kullanıcı gereksinimi karşılamıyorsa veya özel bir kullanıcı değilse yetkilendirme başarısız olacak.
				context.Fail();
			}

			return Task.CompletedTask;
		}
	}

	public class CustomAuthorizationRequirement : IAuthorizationRequirement
	{
		// Belirli gereksinimler eklemek isterseniz burada tanımlayabilirsiniz.
	}

}
