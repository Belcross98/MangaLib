using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vaporAPI.Data;
using vaporAPI.Dtos.Review;
using vaporAPI.Interfaces.Repository;
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
            var user = await _context.Users.FindAsync(review.UserId);

            if (check != null)
                return null;

            review.User = user;
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var check = await _context.Reviews.FindAsync(id);

            if (check == null)
            {
                return null;
            }

            _context.Reviews.Remove(check);
            await _context.SaveChangesAsync();
            return check;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review?> UpdateAsync(int id, UpdateReviewDto updateReviewDto)
        {
            var check = await _context.Reviews.FindAsync(id);

            if (check == null)
                return null;

            check.Rating = updateReviewDto.Rating;
            check.Comment = updateReviewDto.Comment;
            check.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return check;
        }
        public async Task<Manga?> MangaExists(int id)
        {
            return await _context.Mangas.FindAsync(id);
        }
        public async Task<User?> UserExists(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAvgRating(int mangaId)
        {
            var manga = await _context.Mangas.FindAsync(mangaId);

            if (manga == null)
                return;

            var reviews = await _context.Reviews.Where(r => r.MangaId == mangaId).ToListAsync();

            manga.AverageRating = (decimal?)(reviews.Count > 0 ? reviews.Average(r => r.Rating) : 0);

            await _context.SaveChangesAsync();



        }
    }
}