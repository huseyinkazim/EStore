using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Service.ShoppingCartApi.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class ShoppingCartController:ControllerBase
	{
	}
}
