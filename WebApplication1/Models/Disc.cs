using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Disc
    {
        public Disc()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int DiscId { get; set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public virtual Album Album { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
