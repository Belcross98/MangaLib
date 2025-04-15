

using vaporAPI.Dtos.Review;
using vaporAPI.Helpers;

namespace vaporAPI.Interfaces.Service
{
    public interface IReviewService
    {

        Task<ApiResponse<List<ReviewDto>>> GetReviewListAsync();
        Task<ApiResponse<ReviewDto>> GetReviewByIdAsync(int id);
        Task<ApiResponse<ReviewDto>> CreateReviewAsync(CreateReviewDto createReviewDto, string userId);
        Task<ApiResponse<ReviewDto>> DeleteReviewAsync(int id);

    }
}