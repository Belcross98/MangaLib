using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Review;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviews = await _reviewRepo.GetAllAsync();
            var reviewsDto = reviews.Select(r => r.ToReviewDto());

            return Ok(reviewsDto);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var check = await _reviewRepo.GetByIdAsync(id);
            if (check == null)
                return NotFound();

            return Ok(check.ToReviewDto());
        }


        [HttpPost]
        public async Task<IActionResult> createReview([FromBody] CreateReviewDto createReviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var manga = await _reviewRepo.MangaExists(createReviewDto.MangaId);
            var user = await _reviewRepo.UserExists(createReviewDto.UserId);

            if (manga == null)
            {
                return BadRequest("Manga does not exist!");
            }

            if (user == null)
            {
                return BadRequest("User does not exist!");
            }

            var review = await _reviewRepo.CreateAsync(createReviewDto.ToCreateFromDto(user, manga));

            if (review == null)
            {
                return BadRequest("Manga is already rated by user!");
            }

            await _reviewRepo.CreateAsync(review);
            await _reviewRepo.UpdateAvgRating(review.MangaId);


            return CreatedAtAction(nameof(getById), new { id = review.Id }, review.ToReviewDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> updateReview([FromRoute] int id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var update = await _reviewRepo.UpdateAsync(id, updateReviewDto);

            if (update == null)
            {
                return NotFound();
            }

            await _reviewRepo.UpdateAvgRating(update.MangaId);
            return Ok(update.ToReviewDto());


        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deleteReview([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var delete = await _reviewRepo.DeleteAsync(id);

            if (delete == null)
            {
                return NotFound();
            }
            await _reviewRepo.UpdateAvgRating(delete.MangaId);
            return NoContent();

        }

    }
}