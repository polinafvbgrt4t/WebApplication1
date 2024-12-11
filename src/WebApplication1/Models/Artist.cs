using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public int ArtistId { get; set; }
        public string? Nicneym { get; set; }
        public string? Biography { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
