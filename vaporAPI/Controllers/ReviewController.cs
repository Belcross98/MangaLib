using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Review;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Repository;
using vaporAPI.Mappers;



namespace vaporAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepo = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getAllReviews()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            var reviewsDto = reviews.Select(r => r.ToReviewDto());

            return Ok(new ApiResponse<IEnumerable<ReviewDto>>(reviewsDto, true, "Retrieved all reviews"));

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
            var check = await _reviewRepo.GetByIdAsync(id);
            if (check == null)
                return StatusCode(404, new ApiResponse<int>(id, false, "Review with the given id does not exist"));

            return Ok(new ApiResponse<ReviewDto>(check.ToReviewDto(), true, "Review found"));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> createReview([FromBody] CreateReviewDto createReviewDto)
        {
            var manga = await _reviewRepo.MangaExists(createReviewDto.MangaId);

            if (manga == null)
            {
                return BadRequest(new ApiResponse<CreateReviewDto>(createReviewDto, false, "Selected Manga does not exist"));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return BadRequest(new ApiResponse<string>(userId, false, "User with the given id does not exist"));
            }

            var review = await _reviewRepo.CreateAsync(createReviewDto.ToCreateFromDto(userId, manga));

            if (review == null)
            {
                return BadRequest(new ApiResponse<CreateReviewDto>(createReviewDto, false, "Review already rated by user"));
            }

            await _reviewRepo.CreateAsync(review);
            await _reviewRepo.UpdateAvgRating(review.MangaId);


            return StatusCode(201, new ApiResponse<ReviewDto>(review.ToReviewDto(), true, "Review created successfully"));
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> updateReview([FromRoute] int id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            var update = await _reviewRepo.UpdateAsync(id, updateReviewDto);

            if (update == null)
            {
                return StatusCode(404, new ApiResponse<int>(id, false, "Review with the given id does not exist"));
            }

            await _reviewRepo.UpdateAvgRating(update.MangaId);
            return Ok(new ApiResponse<ReviewDto>(update.ToReviewDto(), true, "Review updated successfully"));


        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deleteReview([FromRoute] int id)
        {
            var delete = await _reviewRepo.DeleteAsync(id);

            if (delete == null)
            {
                return StatusCode(404, new ApiResponse<int>(id, false, "Review with the given id does not exist"));
            }
            await _reviewRepo.UpdateAvgRating(delete.MangaId);
            return StatusCode(204, new ApiResponse<ReviewDto>(delete.ToReviewDto(), true, "Review deleted successfully"));

        }

    }
}