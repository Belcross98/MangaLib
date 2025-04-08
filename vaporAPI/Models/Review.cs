using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required int MangaId { get; set; }
        public required int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
        public Manga? Manga { get; set; }

    }
}