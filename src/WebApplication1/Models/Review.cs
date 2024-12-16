using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Review
    {
        public int ReviewsId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string TextReviews { get; set; } = null!;
        public int? Rating { get; set; }
        public DateTime Rev1iewDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
