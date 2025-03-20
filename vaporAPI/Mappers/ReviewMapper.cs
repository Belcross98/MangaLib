using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using vaporAPI.Dtos.Review;
using vaporAPI.Models;

namespace vaporAPI.Mappers
{
    public static class ReviewMapper
    {
        
        public static ReviewDto ToReviewDto(this Review review){
            return new ReviewDto{

                Id = review.Id,
                UserId = review.UserId,
                MangaId = review.MangaId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt

            };
        }

    }
}