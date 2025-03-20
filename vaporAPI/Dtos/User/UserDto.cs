using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.User
{
    public class UserDto
    {
         public int Id { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; } // TODO remove this field later
        public string UserInformation { get; set; } = string.Empty;
        public string UserProfilePictureURL { get; set; } = string.Empty;
    }
}