using System.Text.Json;

namespace Flower_Shop.Models
{
    public class ErrorDetails
    {
        // Код ошибки (HTTP статус)
        public int StatusCode { get; set; }

        // Сообщение для пользователя
        public string Message { get; set; } = string.Empty;

        // RequestId - чтобы найти запись в логах
        public string RequestId { get; set; } = string.Empty;

        // Чтобы вернуть как JSON
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}