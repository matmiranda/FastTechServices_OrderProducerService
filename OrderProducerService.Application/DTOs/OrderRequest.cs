namespace OrderProducerService.Application.DTOs
{
    public class OrderRequest
    {
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = "Received";  // ou você pode deixar isso fixo no backend
        public string DeliveryType { get; set; } = string.Empty; // "Counter", "DriveThru", "Delivery"
        public string? Justification { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new();
        public string Action { get; set; } = "CREATE"; // CREATE or UPDATE
    }
}