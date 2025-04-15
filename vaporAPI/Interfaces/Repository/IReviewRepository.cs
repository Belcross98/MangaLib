using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vaporAPI.Dtos.Review;
using vaporAPI.Models;

namespace vaporAPI.Interfaces.Repository
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<Review?> CreateAsync(Review review);
        Task<Review?> DeleteAsync(Review review);
        Task<Manga?> MangaExists(int id);
        Task<User?> UserExists(string id);
        Task UpdateAsync();

    }
}