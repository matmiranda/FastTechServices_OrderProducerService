using System.ComponentModel.DataAnnotations;

namespace OrderProducerService.Application.Request
{
    public class OrderCancelRequest
    {
        [Range(1, ulong.MaxValue, ErrorMessage = "OrderId deve ser maior que zero.")]
        public ulong OrderId { get; set; }

        [Required(ErrorMessage = "CancelReason é obrigatório.")]
        [StringLength(200, ErrorMessage = "Motivo do cancelamento não pode exceder 200 caracteres.")]
        public string CancelReason { get; set; } = string.Empty;
    }

}
