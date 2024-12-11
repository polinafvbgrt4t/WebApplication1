using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
            Reviews = new HashSet<Review>();
            Shippings = new HashSet<Shipping>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool StatusOrder { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
