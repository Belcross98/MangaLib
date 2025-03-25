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

        public async Task<Review?> CreateAsync(Review review)
        {

            var check = await _context.Reviews.FirstOrDefaultAsync(r => r.MangaId == review.MangaId && r.UserId == review.UserId);

            if (check != null)
                return null;
               
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public Task<Review?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
           return await _context.Reviews.FindAsync(id);
        }

        public Task<Review?> UpdateAsync(int id, UpdateReviewDto updateReviewDto)
        {
            throw new NotImplementedException();
        }
        public async Task<Manga?> MangaExists(int id)
        {
            return await _context.Mangas.FindAsync(id);
        }
        public async Task<User?> UserExists(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}