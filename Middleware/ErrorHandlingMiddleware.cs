using System.Text.Json;
using Flower_Shop.Models;

namespace Flower_Shop.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestId = context.TraceIdentifier;

            try
            {
                await _next(context);

                // Обрабатываем ошибки валидации (статус 400)
                if (context.Response.StatusCode == StatusCodes.Status400BadRequest && !context.Response.HasStarted)
                {
                    await HandleValidationErrorAsync(context, requestId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке запроса {RequestId}", requestId);
                await HandleExceptionAsync(context, requestId, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, string requestId, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Внутренняя ошибка сервера",
                RequestId = requestId
            };

            var jsonResponse = JsonSerializer.Serialize(errorDetails);
            await context.Response.WriteAsync(jsonResponse);
        }

        // 👇 ВОТ ЭТОТ МЕТОД БЫЛ ПОТЕРЯН - ДОБАВЛЯЕМ
        private static async Task HandleValidationErrorAsync(HttpContext context, string requestId)
        {
            context.Response.ContentType = "application/json";

            var errorDetails = new ErrorDetails
            {
                StatusCode = 400,
                Message = "Ошибка валидации данных",
                RequestId = requestId
            };

            var jsonResponse = JsonSerializer.Serialize(errorDetails);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}