# Flower Shop API

Веб-служба для управления каталогом цветочного магазина с хранением данных в памяти.

## Стек технологий
- ASP.NET Core 9
- In-memory хранилище (ConcurrentDictionary)
## 🚀 Запуск проекта

### 📥 Установка
```bash
git clone https://github.com/Ecolis/Flower_Shop.git
cd Flower_Shop
```
▶️ **Запуск**
```bash
dotnet run
```
🌐 **Проверка**

https://localhost:7189/api/flowers - Должно вывести json файл с цветами. Если вывдит, то отлично, программа работает и можно ее тестировать.

## Архитектура решения

Проект построен на основе конвейера middleware, где каждый обработчик решает одну задачу:

1. **RequestLoggingMiddleware** - логирует все входящие запросы и исходящие ответы, измеряет время выполнения
2. **ErrorHandlingMiddleware** - перехватывает исключения и возвращает ошибки в едином формате с RequestId
3. **Контроллер Flowers** - обрабатывает бизнес-логику работы с цветами

Такая архитектура позволяет отделить сквозные задачи (логирование, обработка ошибок) от основной логики приложения.

## Модель данных

```csharp
public class Flower
{
    public int Id { get; set; }              // Уникальный идентификатор
    public string Name { get; set; }          // Название цветка
    public decimal Price { get; set; }        // Цена
    public int Quantity { get; set; }          // Количество в наличии
    public DateTime CreatedAt { get; set; }    // Дата добавления
}
```

## Эндпоинты API

| Метод | URL                 | Описание              |
|:-----:|---------------------|-----------------------|
| GET   | `/api/flowers`      | Получить все цветы    |
| GET   | `/api/flowers/{id}` | Получить цветок по ID |
| POST  | `/api/flowers`      | Добавить новый цветок |## Эндпоинты API


## Пример POST запроса

```json
{
    "name": "Орхидея",
    "price": 500,
    "quantity": 3
}
```

## Валидация

- **Название**: обязательно, от 2 до 100 символов
- **Цена**: обязательна, больше 0
- **Количество**: обязательно, неотрицательное

## Формат ошибок

```json
{
    "statusCode": 404,
    "message": "Цветок с ID 999 не найден",
    "requestId": "0HNJQVGIKC0II:00000001"
}
```
## Проверка работоспособности через Postman

### GET все цветы
- **Method:** GET
- **URL:** https://localhost:7189/api/flowers

### GET по ID
- **Method:** GET
- **URL:** https://localhost:7189/api/flowers/1

### POST новый цветок
- **Method:** POST
- **URL:** https://localhost:7189/api/flowers
- **Headers:**
```text
Content-Type: application/json
```
- **Body (raw JSON):**
```json
{
    "name": "Орхидея",
    "price": 500,
    "quantity": 3
}
```

