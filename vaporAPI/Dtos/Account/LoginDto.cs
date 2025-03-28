using System.ComponentModel.DataAnnotations;

namespace vaporAPI.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}