using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Discs = new HashSet<Disc>();
        }

        public int GenreId { get; set; }
        public string DescriptionGenre { get; set; } = null!;
        public string NameGenre { get; set; } = null!;

        public virtual ICollection<Disc> Discs { get; set; }
    }
}
