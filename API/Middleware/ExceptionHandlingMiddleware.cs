using BusinessLogic.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var response = new
			{
				success = false,
				message = exception.Message,
				errors = (object)null
			};

			switch (exception)
			{
				case BadRequestException:
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					_logger.LogWarning(exception, "Bad request: {Message}", exception.Message);
					break;

				case NotFoundException:
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
					_logger.LogWarning(exception, "Not found: {Message}", exception.Message);
					break;

				case UnauthorizedException:
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					_logger.LogWarning(exception, "Unauthorized: {Message}", exception.Message);
					break;

				default:
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					response = new
					{
						success = false,
						message = "Đã có lỗi xảy ra. Vui lòng thử lại sau.",
						errors = (object)null
					};
					_logger.LogError(exception, "Internal server error: {Message}", exception.Message);
					break;
			}

			var jsonResponse = JsonSerializer.Serialize(response);
			await context.Response.WriteAsync(jsonResponse);
		}
	}
}
