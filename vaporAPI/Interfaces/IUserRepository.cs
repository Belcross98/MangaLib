using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vaporAPI.Dtos.User;
using vaporAPI.Models;

namespace vaporAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> CreateAsync(User user);
        Task<User?> UpdateAsync(int id,UpdateUserDto updateUserDto);
        Task<User?> DeleteAsync(int id);        
    }
}