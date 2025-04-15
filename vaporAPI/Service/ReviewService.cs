using System.Net;
using vaporAPI.Dtos.Review;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Repository;
using vaporAPI.Interfaces.Service;
using vaporAPI.Mappers;
using vaporAPI.Models;

namespace vaporAPI.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepo = reviewRepository;
        }

        public async Task<ApiResponse<ReviewDto>> CreateReviewAsync(CreateReviewDto createReviewDto, string userId)
        {

            if (userId == null)
                return new ApiResponse<ReviewDto>(null, false, "User with the given id does not exist", HttpStatusCode.NotFound);

            var user = await _reviewRepo.UserExists(userId);
            var manga = await _reviewRepo.MangaExists(createReviewDto.MangaId);

            if (manga == null)
                return new ApiResponse<ReviewDto>(null, false, "Manga with the given id does not exist", HttpStatusCode.NotFound);

            var review = createReviewDto.ToCreateFromDto(userId, manga);

            manga.Reviews.Add(review);
            manga.AverageRating = (decimal?)manga.Reviews.Average(r => r.Rating);

            await _reviewRepo.CreateAsync(review);

            return new ApiResponse<ReviewDto>(review.ToReviewDto(), true, "Review created successfully", HttpStatusCode.OK);
        }

        public async Task<ApiResponse<ReviewDto>> DeleteReviewAsync(int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null)
                return new ApiResponse<ReviewDto>(null, false, "Review with the given id does not exist", HttpStatusCode.NotFound);
            await _reviewRepo.DeleteAsync(review);
            return new ApiResponse<ReviewDto>(review.ToReviewDto(), true, "Successfully deleted a review", HttpStatusCode.OK);
        }

        public async Task<ApiResponse<ReviewDto>> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null)
                return new ApiResponse<ReviewDto>(null, false, "Review with the given id does not exist", HttpStatusCode.NotFound);
            return new ApiResponse<ReviewDto>(review.ToReviewDto(), true, "Review retrieved successfully", HttpStatusCode.OK);
        }

        public async Task<ApiResponse<List<ReviewDto>>> GetReviewListAsync()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            var reviewsDto = reviews.Select(r => r.ToReviewDto()).ToList();
            return new ApiResponse<List<ReviewDto>>(reviewsDto, true, "Successfuly retrieved Reviews", HttpStatusCode.OK);
        }
    }
}