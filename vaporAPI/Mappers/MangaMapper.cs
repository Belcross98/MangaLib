using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vaporAPI.Dtos.Manga;
using vaporAPI.Models;

namespace vaporAPI.Mappers
{
    public static class MangaMapper
    {
        public static MangaDto ToMangaDto(this Manga manga){

            return new MangaDto{

              Id = manga.Id,
              Name = manga.Name,
              Description = manga.Description,
              MangaPictureURL = manga.MangaPictureURL,
              AverageRating = manga.AverageRating,
              Reviews = manga.Reviews.Select(m => m.ToReviewDto()).ToList() 

            };
        }

        public static Manga ToCreateFromDto(this CreateMangaDto createMangaDto){
            return new Manga{
                Name = createMangaDto.Name,
                Description = createMangaDto.Description,
                MangaPictureURL = createMangaDto.MangaPictureURL
            };
        }
    }
}