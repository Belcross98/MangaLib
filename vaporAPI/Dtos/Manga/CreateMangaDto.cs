using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Dtos.Manga
{
    public class CreateMangaDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Manga name can't be longer than 200 characters")]
        public required string Name { get; set; }
        [MaxLength(1000, ErrorMessage = "Description can't be longer than 1000 characters")]
        public string Description { get; set; } = string.Empty;
        public string MangaPictureURL { get; set; } = string.Empty;

    }
}