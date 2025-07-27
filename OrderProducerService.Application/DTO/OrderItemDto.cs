using System.ComponentModel.DataAnnotations;

namespace OrderProducerService.Application.Dto
{
    public class OrderItemDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "MenuItemId deve ser maior que zero")]
        public long MenuItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal PriceAtOrder { get; set; }
    }
}
