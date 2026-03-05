using System.ComponentModel.DataAnnotations;

namespace Flower_Shop.Models
{
    public class CreateFlowerDto
    {
        // Required значит "обязательное поле"
        [Required(ErrorMessage = "Название цветка обязательно")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Цена обязательна")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Количество обязательно")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
        public int Quantity { get; set; }
    }
}
