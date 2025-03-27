using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.Review
{
    public class CreateReviewDto
    {
        [Required]
        public required string UserId { get; set; }
        [Required]
        public int MangaId { get; set; }
        [Required]
        [Range(1, 5)]
        public required int Rating { get; set; }
        [MaxLength(250, ErrorMessage = "Comment can't be longer than 250 characters.")]
        public string Comment { get; set; } = string.Empty;

    }
}