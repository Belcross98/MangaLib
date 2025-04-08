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

        public static ReviewDto ToReviewDto(this Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                MangaId = review.MangaId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,

            };
        }

        public static Review ToCreateFromDto(this CreateReviewDto createReviewDto, string userId, Manga manga)
        {

            return new Review
            {

                UserId = userId,
                MangaId = createReviewDto.MangaId,
                Rating = createReviewDto.Rating,
                Comment = createReviewDto.Comment,
                Manga = manga,
                CreatedAt = DateTime.UtcNow

            };
        }

    }
}