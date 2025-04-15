using System.ComponentModel.DataAnnotations;

namespace vaporAPI.Dtos.Manga
{
    public class UpdateMangaDto
    {
        [MaxLength(100, ErrorMessage = "Manga name can't be longer than 100 characters")]
        public required string Name { get; set; }
        [MaxLength(250, ErrorMessage = "Description can't be longer than 250 characters.")]
        public string Description { get; set; } = string.Empty;
        public string MangaPictureURL { get; set; } = string.Empty;
    }
}