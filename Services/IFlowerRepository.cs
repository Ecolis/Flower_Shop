using Flower_Shop.Models;
namespace Flower_Shop.Services
{
    public interface IFlowerRepository
    {
        // Получить все цветы
        IEnumerable<Flower> GetAll();

        // Найти один цветок по ID
        Flower? GetById(int id);

        // Добавить новый цветок
        Flower Add(Flower flower);
    }
}
