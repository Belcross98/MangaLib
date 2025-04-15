using System.ComponentModel.DataAnnotations;


namespace vaporAPI.Dtos.Manga
{
    public class CreateMangaDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Manga name can't be longer than 200 characters")]
        [MinLength(2, ErrorMessage = "Name must at least have 2 characters")]
        public required string Name { get; set; }
        [MaxLength(1000, ErrorMessage = "Description can't be longer than 1000 characters")]
        public string Description { get; set; } = string.Empty;
        public string MangaPictureURL { get; set; } = string.Empty;

    }
}