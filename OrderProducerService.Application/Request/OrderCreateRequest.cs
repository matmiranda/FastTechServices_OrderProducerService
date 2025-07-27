using OrderProducerService.Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace OrderProducerService.Application.Request
{
    public class OrderCreateRequest
    {
        [Required]
        [Range(1, ulong.MaxValue, ErrorMessage = "CustomerId deve ser maior que zero")]
        public ulong CustomerId { get; set; }

        [Required(ErrorMessage = "O método de entrega é obrigatório")]
        [RegularExpression("BALCAO|DRIVE_THRU|DELIVERY", ErrorMessage = "DeliveryMethod inválido")]
        public required string DeliveryMethod { get; set; }

        [Required(ErrorMessage = "Itens do pedido são obrigatórios")]
        [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um item")]
        public required List<OrderItemDto> Items { get; set; }

    }

}