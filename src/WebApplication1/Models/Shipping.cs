using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Shipping
    {
        public int ShippingId { get; set; }
        public int OrderId { get; set; }
        public DateTime ShippingDate { get; set; }
        public string DeliveryServise { get; set; } = null!;
        public string? TrecerShipping { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
