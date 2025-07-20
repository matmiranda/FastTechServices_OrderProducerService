using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProducerService.Application.DTOs
{
    public class OrderItemRequest
    {
        public Guid MenuItemId { get; set; }  // ID do item do cardápio
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
