
namespace vaporAPI.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public required string UserId { get; set; }
        public required User User { get; set; }
    }
}