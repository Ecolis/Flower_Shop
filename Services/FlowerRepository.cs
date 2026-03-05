using Flower_Shop.Models;
using System.Collections.Concurrent; // Для потокобезопасной коллекции
namespace Flower_Shop.Services
{
    public class FlowerRepository : IFlowerRepository
    {
        // Это наш "склад" в памяти
        // ConcurrentDictionary - специальная коллекция, которая выдерживает 
        // одновременные запросы от нескольких пользователей
        private readonly ConcurrentDictionary<int, Flower> _flowers = new();

        // Счётчик для новых ID (чтобы каждый цветок имел уникальный номер)
        private int _nextId = 1;

        public FlowerRepository()
        {
            Add(new Flower { Name = "Роза красная", Price = 150, Quantity = 10 });
            Add(new Flower { Name = "Тюльпан жёлтый", Price = 80, Quantity = 25 });
            Add(new Flower { Name = "Ромашка", Price = 50, Quantity = 30 });
        }
        public IEnumerable<Flower> GetAll()
        {
            // Возвращаем все значения из словаря (наши цветы)
            return _flowers.Values;
        }

        public Flower? GetById(int id)
        {
            // Пытаемся найти цветок по ID
            // Если находим - возвращаем, если нет - null
            _flowers.TryGetValue(id, out var flower);
            return flower;
        }

        public Flower Add(Flower flower)
        {
            // Присваиваем новый ID
            flower.Id = _nextId++;

            // Ставим дату создания на текущий момент
            flower.CreatedAt = DateTime.Now;

            // Добавляем в словарь
            _flowers.TryAdd(flower.Id, flower);

            // Возвращаем цветок уже с ID
            return flower;
        }


    }
}
