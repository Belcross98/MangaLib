
namespace vaporAPI.Dtos.Account
{
    public class NewUserDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Tokens { get; set; }
    }
}