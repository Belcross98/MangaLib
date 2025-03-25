using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using vaporAPI.Dtos.Review;
using vaporAPI.Interfaces;
using vaporAPI.Mappers;
using vaporAPI.Models;
using vaporAPI.Repository;

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
        public async Task<IActionResult> getAllReviews(){

            var reviews = await _reviewRepo.GetAllAsync();
            var reviewsDto = reviews.Select(r => r.ToReviewDto());
            
            return Ok(reviewsDto);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] int id){

            var check = await _reviewRepo.GetByIdAsync(id);
            if(check == null)
                return NotFound();
            
            return Ok(check.ToReviewDto());
        }


        [HttpPost]
        public async Task<IActionResult> createReview([FromBody] CreateReviewDto createReviewDto){

            var manga = await _reviewRepo.MangaExists(createReviewDto.MangaId);
            var user = await _reviewRepo.UserExists(createReviewDto.UserId);

            if(manga == null){
                return BadRequest("Manga does not exist!");
            }

            if(user == null){
                return BadRequest("User does not exist!");
            } 

            var review = createReviewDto.ToCreateFromDto(user,manga);

            if(review == null){
                return BadRequest("Manga is already rated by user!");
            }

            await _reviewRepo.CreateAsync(review);


            return CreatedAtAction(nameof(getById),new {id = review.Id} ,review.ToReviewDto());
        }


    }
}