namespace Flower_Shop.Models
{
    public class Flower
    {
        // ID - нужно, чтобы отличать один цветок от другого
        public int Id { get; set; }

        // Название цветка (роза, тюльпан и т.д.) - непустое (правило 1)
        public string Name { get; set; } = string.Empty;

        // Цена за штуку - неотрицательная (правило 2)
        public decimal Price { get; set; }

        // Количество в наличии - тоже неотрицательное (правило 3)
        public int Quantity { get; set; }

        // Когда добавили в каталог
        public DateTime CreatedAt { get; set; }
    }
}
