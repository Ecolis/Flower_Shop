# Flower Shop API

Веб-служба для управления каталогом цветочного магазина с хранением данных в памяти.

## Стек технологий
- ASP.NET Core 9
- In-memory хранилище (ConcurrentDictionary)
## 🚀 Запуск проекта

### 📥 Установка
```bash
git clone https://github.com/your-username/your-repo.git
cd your-repo
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

