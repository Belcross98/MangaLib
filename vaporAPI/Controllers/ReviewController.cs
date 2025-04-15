using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Review;
using vaporAPI.Helpers;
using vaporAPI.Interfaces.Service;



namespace vaporAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllReviews()
        {
            try
            {
                var response = await _reviewService.GetReviewListAsync();
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
            try
            {
                var response = await _reviewService.GetReviewByIdAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> createReview([FromBody] CreateReviewDto createReviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var response = await _reviewService.CreateReviewAsync(createReviewDto, userId);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }


        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deleteReview([FromRoute] int id)
        {
            try
            {
                var response = await _reviewService.DeleteReviewAsync(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                var errorRes = new ApiResponse<Exception>(e, false, e.Message, HttpStatusCode.InternalServerError);
                return StatusCode(errorRes.StatusCode, errorRes);
            }

        }

    }
}