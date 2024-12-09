using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Album
    {
        public Album()
        {
            Discs = new HashSet<Disc>();
        }

        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; } = null!;
        public int? GenreId { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual ICollection<Disc> Discs { get; set; }
    }
}
