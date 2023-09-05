using Microsoft.AspNetCore.Identity;

namespace EStore.Service.AuthApi.Models
{
	public class ApplicationUser:IdentityUser
	{
		public string Name{ get; set; }
	}
}
