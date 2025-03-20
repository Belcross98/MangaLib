using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vaporAPI.Data;
using vaporAPI.Dtos.User;
using vaporAPI.Interfaces;
using vaporAPI.Models;

namespace vaporAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;

        }

        public async Task<User?> CreateAsync(User user)
        {
            var check = await _context.Users.FirstOrDefaultAsync(m => m.Username == user.Username);

            if (check != null)
                return null;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<User?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateAsync(int id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}