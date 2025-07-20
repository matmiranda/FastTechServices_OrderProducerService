
namespace OrderProducerService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string DeliveryType { get; set; }
        public string? Justification { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItems> Items { get; set; }
    }
}