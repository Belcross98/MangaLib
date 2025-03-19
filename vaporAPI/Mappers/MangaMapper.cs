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

              Name = manga.Name,
              Rating = manga.Rating  

            };
        }

        public static Manga ToCreateMangaDto(this CreateMangaDto createMangaDto){
            return new Manga{
                Name = createMangaDto.Name
            };
        }
    }
}