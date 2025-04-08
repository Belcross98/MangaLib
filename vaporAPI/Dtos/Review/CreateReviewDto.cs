using System.ComponentModel.DataAnnotations;

namespace vaporAPI.Dtos.Review
{
    public class CreateReviewDto
    {
        [Required]
        public int MangaId { get; set; }
        [Required]
        [Range(1, 5)]
        public required int Rating { get; set; }
        [MaxLength(250, ErrorMessage = "Comment can't be longer than 250 characters.")]
        public string Comment { get; set; } = string.Empty;

    }
}