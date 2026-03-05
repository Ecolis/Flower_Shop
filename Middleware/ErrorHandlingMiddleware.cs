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
            // Получаем или создаем RequestId
            var requestId = context.TraceIdentifier;

            try
            {
                // Передаем запрос дальше по конвейеру
                await _next(context);
            }
            catch (Exception ex)
            {
                // Логируем ошибку с RequestId
                _logger.LogError(ex, "Ошибка при обработке запроса {RequestId}", requestId);

                // Отправляем клиенту красивый ответ
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
                Message = "Внутренняя ошибка сервера", // Не показываем детали пользователю
                RequestId = requestId
            };

            var jsonResponse = JsonSerializer.Serialize(errorDetails);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}