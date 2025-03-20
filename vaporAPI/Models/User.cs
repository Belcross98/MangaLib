using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string UserInformation { get; set; } = string.Empty;
        public string UserProfilePictureURL { get; set; } = string.Empty;

        public List<Review> Reviews { get; set; } = new List<Review>();
        
    }
}