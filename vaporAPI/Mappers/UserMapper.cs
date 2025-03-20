using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vaporAPI.Dtos.User;
using vaporAPI.Models;

namespace vaporAPI.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user){

            return new UserDto{
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                UserInformation = user.UserInformation,
                UserProfilePictureURL = user.UserProfilePictureURL
            };
        }
    }
}