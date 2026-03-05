using System.Diagnostics;

namespace Flower_Shop.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestId = context.TraceIdentifier;
            var method = context.Request.Method;
            var path = context.Request.Path;

            // Логируем начало запроса
            _logger.LogInformation("Начат запрос {RequestId}: {Method} {Path}", requestId, method, path);

            // Засекаем время
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Передаём запрос дальше
                await _next(context);
            }
            finally
            {
                // Останавливаем таймер в любом случае (даже если ошибка)
                stopwatch.Stop();

                var statusCode = context.Response.StatusCode;

                // Логируем завершение с временем
                _logger.LogInformation("Завершён запрос {RequestId}: {Method} {Path} = {StatusCode} за {ElapsedMs}мс",
                    requestId, method, path, statusCode, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
