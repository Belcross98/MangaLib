using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vaporAPI.Data;
using vaporAPI.Dtos.Review;
using vaporAPI.Interfaces;
using vaporAPI.Models;

namespace vaporAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {

        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Task<Review?> CreateAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Review?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public Task<Review?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Review?> UpdateAsync(int id, UpdateReviewDto updateReviewDto)
        {
            throw new NotImplementedException();
        }
    }
}