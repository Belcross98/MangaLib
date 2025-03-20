using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vaporAPI.Dtos.Review;
using vaporAPI.Models;

namespace vaporAPI.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<Review?> CreateAsync(Review review);
        Task<Review?> UpdateAsync(int id,UpdateReviewDto updateReviewDto);
        Task<Review?> DeleteAsync(int id);       
    }
}