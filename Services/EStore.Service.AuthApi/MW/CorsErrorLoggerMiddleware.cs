
public class CorsErrorLoggerMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<CorsErrorLoggerMiddleware> _logger;

	public CorsErrorLoggerMiddleware(RequestDelegate next, ILogger<CorsErrorLoggerMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			if (IsCorsError(ex))
			{
				_logger.LogError($"CORS Hatası: {ex.Message}");
			}
			else
			{
				throw;
			}
		}
	}

	private bool IsCorsError(Exception ex)
	{
		// CORS hatası olup olmadığını kontrol etmek için özel bir koşul ekleyebilirsiniz.
		// Örneğin, istisna mesajında CORS ile ilgili belirli bir metni arayabilirsiniz.
		return ex.Message.Contains("CORS ile ilgili metin");
	}
}
