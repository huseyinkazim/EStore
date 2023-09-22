using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace EStore.Service.ShoppingCartApi.Extension
{
	public class ApiHttpClientHandler: DelegatingHandler
	{
		private readonly IHttpContextAccessor _accessor;

		public ApiHttpClientHandler(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			// IHttpContextAccessor ile mevcut HTTP isteği ile ilgili bilgilere erişebiliriz.
			var httpContext = _accessor.HttpContext;

			// Örnek: Mevcut kullanıcının kimliğini almak
			var userId = httpContext.User.Identity.Name;

			// Örnek: Mevcut isteğin IP adresini almak
			var clientIp = httpContext.Connection.RemoteIpAddress.ToString();

			// Başka işlemler yapabilirsiniz...
			var token = await _accessor.HttpContext.GetTokenAsync("access_token");

			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
