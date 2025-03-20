using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.Review
{
    public class CreateReviewDto
    {
        public int UserId { get; set; }
        public int MangaId { get; set; }
        public required int Rating {get;set;}
        public string Comment { get; set; } = string.Empty;   
    }
}