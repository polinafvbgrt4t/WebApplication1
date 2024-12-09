using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int DiscId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Disc Disc { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
