using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MangaId { get; set; }
        public required int Rating {get;set;}
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        public required User User { get; set; }
        public required Manga Manga { get; set; }

    }
}