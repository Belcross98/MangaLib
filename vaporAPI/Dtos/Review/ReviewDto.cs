
namespace vaporAPI.Dtos.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int MangaId { get; set; }
        public required int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}