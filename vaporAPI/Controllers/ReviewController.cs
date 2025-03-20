using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
            Console.WriteLine("DOsao do vode");
            var reviewsDto = reviews.Select(r => r.ToReviewDto());
            
            return Ok(reviewsDto);

        }


    }
}