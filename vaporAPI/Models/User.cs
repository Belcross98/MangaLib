using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace vaporAPI.Models
{
    public class User : IdentityUser
    {

        public string UserInformation { get; set; } = string.Empty;
        public string UserProfilePictureURL { get; set; } = string.Empty;
        public List<Review> Reviews { get; set; } = new List<Review>();
        public RefreshToken? refreshToken { get; set; }

    }
}