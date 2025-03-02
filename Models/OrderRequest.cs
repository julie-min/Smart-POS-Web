﻿using System.Collections.Generic;

namespace SmartPOSWeb.Models
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }
    }

    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
