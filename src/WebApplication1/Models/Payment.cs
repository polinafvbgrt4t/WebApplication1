using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public decimal Amount { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
